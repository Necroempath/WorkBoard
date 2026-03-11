using WorkBoard.Domain;

namespace WorkBoard.Application.Abstractions.Repositories;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken ct);
    Task<RefreshToken> AddTokenAsync(RefreshToken token, CancellationToken ct);
    Task<bool> DeleteTokenAsync(string token, CancellationToken ct);
}
