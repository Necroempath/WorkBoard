using WorkBoard.Domain;

namespace WorkBoard.Application.Interfaces;

public interface IUserRepository
{
    Task<User> GetByIdAsync(Guid id, CancellationToken token);
    Task<User> GetByEmailAsync(string email, CancellationToken token);
    Task<User> AddAsync(User user, CancellationToken token);
}
