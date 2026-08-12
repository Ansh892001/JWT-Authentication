using EmployeeManagement.Api.Exceptions;
using EmployeeManagement.Api.Models.Entities;
using EmployeeManagement.Api.Models.Requests;
using EmployeeManagement.Api.Models.Responses;
using EmployeeManagement.Api.Repositories.Interfaces;
using EmployeeManagement.Api.Services.Interfaces;
using Microsoft.Extensions.Options;
using EmployeeManagement.Api.Configuration;

namespace EmployeeManagement.Api.Services;

public class AuthService : IAuthService
{
    private readonly ITokenService _tokenService;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUserRepository _userRepository;
    private readonly JwtSettings _jwtSettings;
    private readonly IPasswordHasher _passwordHasher;


    public AuthService(
        IUserRepository userRepository,
        ITokenService tokenService,
        IRefreshTokenRepository refreshTokenRepository,
        IPasswordHasher passwordHasher,
        IOptions<JwtSettings> options)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _refreshTokenRepository = refreshTokenRepository;
        _passwordHasher = passwordHasher;
        _jwtSettings = options.Value;
    }
    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (!_passwordHasher.VerifyPassword(
        request.Password,
        user.Password))
        {
            throw new InvalidCredentialsException();
        }
        return await IssueTokensAsync(user);
    }

    public async Task<AuthResponse> RefreshAsync(RefreshRequest request)
    {
        var existingRefreshToken =
            await _refreshTokenRepository.GetByTokenAsync(request.RefreshToken);

        if (existingRefreshToken is null)
            throw new InvalidCredentialsException("Invalid refresh token.");

        if (existingRefreshToken.IsRevoked)
            throw new InvalidCredentialsException("Refresh token has been revoked.");

        if (existingRefreshToken.ExpiresAt <= DateTime.UtcNow)
            throw new InvalidCredentialsException("Refresh token has expired.");

        var user =
            await _userRepository.GetByIdAsync(existingRefreshToken.UserId);

        if (user is null)
            throw new InvalidCredentialsException("User not found.");

        existingRefreshToken.IsRevoked = true;
        existingRefreshToken.RevokedAt = DateTime.UtcNow;

        var response = await IssueTokensAsync(user);

        existingRefreshToken.ReplacedByToken = response.RefreshToken;

        await RevokeRefreshTokenAsync(
            existingRefreshToken,
            response.RefreshToken);

        await _refreshTokenRepository.UpdateAsync(existingRefreshToken);

        return response;
    }

    public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
    {
        // 1. Check duplicate email
        var existingUser =
            await _userRepository.GetByEmailAsync(request.Email);

        if (existingUser != null)
        {
            throw new UserAlreadyExistsException();
        }

        // 2. Hash password
        var hashedPassword =
            _passwordHasher.HashPassword(request.Password);

        // 3. Create entity
        var user = new User
        {
            Email = request.Email,
            Password = hashedPassword,
            Role = "User"
        };

        // 4. Save
        await _userRepository.AddAsync(user);

        // 5. Return response
        return new RegisterResponse
        {
            Message = "User registered successfully."
        };
    }

    private async Task<AuthResponse> IssueTokensAsync(User user)
    {
        var now = DateTime.UtcNow;

        var accessTokenExpiry =
            now.AddMinutes(_jwtSettings.AccessTokenExpiryMinutes);

        var refreshTokenExpiry =
            now.AddDays(_jwtSettings.RefreshTokenExpiryDays);

        var accessToken =
            _tokenService.GenerateAccessToken(user, accessTokenExpiry);

        var refreshToken =
            _tokenService.GenerateRefreshToken();

        var refreshTokenEntity = new RefreshToken
        {
            Token = refreshToken,
            UserId = user.Id,
            CreatedAt = now,
            ExpiresAt = refreshTokenExpiry,
            IsRevoked = false
        };

        await _refreshTokenRepository.SaveAsync(refreshTokenEntity);

        return new AuthResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            AccessTokenExpiresAt = accessTokenExpiry,
            RefreshTokenExpiresAt = refreshTokenExpiry
        };
    }

    private async Task RevokeRefreshTokenAsync(
    RefreshToken refreshToken,
    string? replacedByToken = null)
    {
        refreshToken.IsRevoked = true;

        refreshToken.RevokedAt = DateTime.UtcNow;

        refreshToken.ReplacedByToken = replacedByToken;

        await _refreshTokenRepository.UpdateAsync(refreshToken);
    }
}