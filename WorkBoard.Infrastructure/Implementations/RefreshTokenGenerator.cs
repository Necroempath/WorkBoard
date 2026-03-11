using Microsoft.Extensions.Options;
using WorkBoard.Application.Abstractions;
using WorkBoard.Domain;
using WorkBoard.Infrastructure.Contracts;

namespace WorkBoard.Infrastructure.Implementations;

public sealed class RefreshTokenGenerator : IRefreshTokenGenerator
{
    private readonly RefreshTokenSettings _settings;

    public RefreshTokenGenerator(IOptions<RefreshTokenSettings> settings)
    {
        _settings = settings.Value;
    }

    public RefreshToken Generate(User user)
    {
        var expiresAt = DateTime.UtcNow.AddDays(_settings.ExpirationDays);

        return new(user.Id, expiresAt);
    }
}
