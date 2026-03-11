using Microsoft.EntityFrameworkCore;
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

    public async Task<RefreshToken?> GetByTokenAsync(Guid token, CancellationToken ct)
    {
        return await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token);
    }

    public async Task<RefreshToken> AddTokenAsync(RefreshToken token, CancellationToken ct)
    {
        await _context.AddAsync(token, ct);

        await _context.SaveChangesAsync();

        return token;
    }

    public async Task<bool> DeleteTokenAsync(Guid token, CancellationToken ct)
    {
        var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token);

        if (refreshToken is null)
            return false;

        _context.RefreshTokens.Remove(refreshToken);

        await _context.SaveChangesAsync();

        return true;
    }
}