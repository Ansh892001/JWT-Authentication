namespace EmployeeManagement.Api.Models.Entities;

public class RefreshToken
{
    public string Token { get; set; } = string.Empty;

    public int UserId { get; set; }

    public DateTime ExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsRevoked { get; set; }

    public DateTime? RevokedAt { get; set; }

    public string? ReplacedByToken { get; set; }
}