using WorkBoard.Domain;

namespace WorkBoard.Application.Abstractions;

public interface IRefreshTokenGenerator
{
    RefreshToken Generate(User user);
}
