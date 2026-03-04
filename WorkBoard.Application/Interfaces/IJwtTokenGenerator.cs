using WorkBoard.Domain;

namespace WorkBoard.Application.Interfaces;

public interface IJwtTokenGenerator
{
    string Generate(User user);
}
