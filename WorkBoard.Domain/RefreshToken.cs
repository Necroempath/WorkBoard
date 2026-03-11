namespace WorkBoard.Domain;

public sealed class RefreshToken
{
    public Guid Token { get; } = Guid.NewGuid();
    public DateTimeOffset ExpiresAt { get; }
    public DateTimeOffset? RevokedAt { get; private set; }
    public Guid? ReplacedByTokenId { get; private set; }
    public Guid UserId { get; }
    public User User { get; } = null!;

    private RefreshToken() { }

    public RefreshToken(Guid userId, DateTimeOffset expiresAt)
    {

        if (userId == Guid.Empty)
            throw new ArgumentException("User Id can not be empty");

        if (expiresAt <= DateTimeOffset.UtcNow)
            throw new ArgumentException("Expire date must be in the future");

        UserId = userId;
        ExpiresAt = expiresAt;
    }

    public void Revoke()
    {
        RevokedAt = DateTimeOffset.UtcNow;
    }

    public void SetReplaceToken(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Token Id cannot be empty");

        ReplacedByTokenId = id;
    }
}
