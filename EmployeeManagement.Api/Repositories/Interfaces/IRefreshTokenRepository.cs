using EmployeeManagement.Api.Models.Entities;

namespace EmployeeManagement.Api.Repositories.Interfaces;

public interface IRefreshTokenRepository
{
    Task SaveAsync(RefreshToken refreshToken);

    Task<RefreshToken?> GetByTokenAsync(string token);

    Task UpdateAsync(RefreshToken refreshToken);

    Task RevokeAllByUserIdAsync(int userId);
}