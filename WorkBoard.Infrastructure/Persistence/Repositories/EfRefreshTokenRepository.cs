using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using WorkBoard.Application.Abstractions.Repositories;
using WorkBoard.Domain;

namespace WorkBoard.Infrastructure.Persistence.Repositories;

public sealed class EfRefreshTokenRepository : IRefreshTokenRepository
{
    private readonly WorkBoardDbContext _context;

    public EfRefreshTokenRepository(WorkBoardDbContext context)
    {
        _context = context;
    }

    public async Task<RefreshToken?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Id == id);
    }

    public async Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken ct)
    {
        var hash = Convert.ToHexString(
            SHA256.HashData(Encoding.UTF8.GetBytes(token)));

        return await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.TokenHash == hash);
    }

    public async Task<RefreshToken> AddTokenAsync(RefreshToken token, CancellationToken ct)
    {
        await _context.AddAsync(token, ct);

        await _context.SaveChangesAsync();

        return token;
    }

    public async Task<bool> DeleteTokenAsync(Guid id, CancellationToken ct)
    {
        var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Id == id);

        if (refreshToken is null)
            return false;

        _context.RefreshTokens.Remove(refreshToken);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}