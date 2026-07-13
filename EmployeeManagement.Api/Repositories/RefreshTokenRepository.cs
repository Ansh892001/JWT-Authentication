using EmployeeManagement.Api.Models.Entities;
using EmployeeManagement.Api.Repositories.Interfaces;

namespace EmployeeManagement.Api.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private static readonly List<RefreshToken> _tokens = new();

    public Task SaveAsync(RefreshToken refreshToken)
    {
        _tokens.Add(refreshToken);
        return Task.CompletedTask;
    }

    public Task<RefreshToken?> GetByTokenAsync(string token)
    {
        var refreshToken = _tokens.FirstOrDefault(t => t.Token == token);
        return Task.FromResult(refreshToken);
    }

    public Task UpdateAsync(RefreshToken refreshToken)
    {
        // Nothing needed because the object reference is already updated.
        return Task.CompletedTask;
    }
}