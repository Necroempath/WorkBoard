namespace WorkBoard.Domain;

public sealed class RefreshToken
{
    public string Token { get; } = string.Empty;
    public DateTimeOffset ExpiresAt { get; }
    public Guid UserId { get; }
    public User User { get; } = null!;

    private RefreshToken() { }

    public RefreshToken(string token, Guid userId, DateTimeOffset expiresAt)
    {
        if (string.IsNullOrWhiteSpace(token))
            throw new ArgumentException("Refresh token cannot be empty");

        if (userId == Guid.Empty)
            throw new ArgumentException("User Id can not be empty");

        if (expiresAt <= DateTime.UtcNow)
            throw new ArgumentException("Expire date must be in the future");

        Token = token;
        UserId = userId;
        ExpiresAt = expiresAt;
    }
}
