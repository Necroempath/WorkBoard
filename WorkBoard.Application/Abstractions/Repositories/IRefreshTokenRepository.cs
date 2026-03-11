using WorkBoard.Domain;

namespace WorkBoard.Application.Abstractions.Repositories;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetByTokenAsync(Guid token, CancellationToken ct);
    Task<RefreshToken> AddTokenAsync(RefreshToken token, CancellationToken ct);
    Task<bool> DeleteTokenAsync(Guid token, CancellationToken ct);
}
