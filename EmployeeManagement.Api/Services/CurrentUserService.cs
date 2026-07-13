using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(
        IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int UserId =>
        int.Parse(
            _httpContextAccessor.HttpContext!
            .User
            .FindFirst(JwtRegisteredClaimNames.Sub)!
            .Value);

    public string Email =>
        _httpContextAccessor.HttpContext!
            .User
            .FindFirst(JwtRegisteredClaimNames.Email)!
            .Value;

    public string Role =>
        _httpContextAccessor.HttpContext!
            .User
            .FindFirst(ClaimTypes.Role)!
            .Value;
}