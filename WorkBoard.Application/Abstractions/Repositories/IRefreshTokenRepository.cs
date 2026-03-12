using WorkBoard.Domain;

namespace WorkBoard.Application.Abstractions.Repositories;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken ct);
    Task<RefreshToken> AddTokenAsync(RefreshToken token, CancellationToken ct);
    Task<bool> DeleteTokenAsync(Guid id, CancellationToken ct);
    Task Save();
}
