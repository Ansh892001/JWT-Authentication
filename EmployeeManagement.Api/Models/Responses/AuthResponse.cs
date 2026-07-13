namespace EmployeeManagement.Api.Models.Responses;

public class AuthResponse
{
    public required string AccessToken { get; set; } = string.Empty;

    public required string RefreshToken { get; set; } = string.Empty;

    public required DateTime AccessTokenExpiresAt { get; set; }

    public required DateTime RefreshTokenExpiresAt { get; set; }

}