using WorkBoard.Domain;

namespace WorkBoard.Application.Entities;

public sealed class RefreshToken
{
    public string Token { get; } = string.Empty;
    public DateTimeOffset ExpiresAt { get; }
    public Guid UserId { get; }
    public User User { get; } = null!;

    private RefreshToken() { }

    public RefreshToken(string token, Guid userId, DateTimeOffset expiresAt)
    {
        if (userId == Guid.Empty)
            throw new InvalidOperationException("User Id can not be empty");

        if (expiresAt <= DateTime.UtcNow)
            throw new InvalidOperationException("Expire date must be in the future");

        Token = token;
        UserId = userId;
        ExpiresAt = expiresAt;
    }
}
