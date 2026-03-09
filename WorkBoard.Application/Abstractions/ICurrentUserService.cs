namespace WorkBoard.Application.Abstractions;

public interface ICurrentUserService
{
    string? UserId { get; }
    string? UserName { get; }
    string? UserEmail { get; }
    bool IsAuthenticated { get; }
}
