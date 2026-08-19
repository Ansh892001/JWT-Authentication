using EmployeeManagement.Api.Contexts;
using EmployeeManagement.Api.Models.Entities;
using EmployeeManagement.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace EmployeeManagement.Api.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private static readonly List<RefreshToken> _tokens = new();

    private readonly ApplicationDbContext _context;

    public RefreshTokenRepository(
        ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task SaveAsync(RefreshToken refreshToken)
    {
        await _context.RefreshTokens.AddAsync(refreshToken);

        await _context.SaveChangesAsync();
    }
    public async Task<RefreshToken?> GetByTokenAsync(string token)
    {
        return await _context.RefreshTokens
            .FirstOrDefaultAsync(x => x.Token == token);
    }

    public async Task UpdateAsync(RefreshToken refreshToken)
    {
        await _context.SaveChangesAsync();
    }

    public async Task RevokeAllByUserIdAsync(int userId)
    {
        await _context.RefreshTokens
            .Where(x =>
                x.UserId == userId &&
                !x.IsRevoked)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(x => x.IsRevoked, true)
                .SetProperty(x => x.RevokedAt, DateTime.UtcNow));
    }
}