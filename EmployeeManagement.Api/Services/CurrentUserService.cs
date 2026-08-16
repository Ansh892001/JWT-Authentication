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
        int.Parse(GetRequiredClaim(ClaimTypes.NameIdentifier).Value);

    public string Email =>
        GetRequiredClaim(ClaimTypes.Email).Value;

    public string Role =>
        GetRequiredClaim(ClaimTypes.Role).Value;

    private Claim GetRequiredClaim(string claimType)
    {
        return _httpContextAccessor.HttpContext?.User.FindFirst(claimType)
            ?? throw new UnauthorizedAccessException("User is not authenticated.");
    }
}