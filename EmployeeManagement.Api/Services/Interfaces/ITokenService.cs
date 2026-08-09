using EmployeeManagement.Api.Models.Entities;

namespace EmployeeManagement.Api.Services.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(User user, DateTime expiresAt);

    string GenerateRefreshToken();
}