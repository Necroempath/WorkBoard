using Microsoft.EntityFrameworkCore;
using WorkBoard.Application.Interfaces;
using WorkBoard.Domain;

namespace WorkBoard.Infrastructure.Persistence;

public sealed class EfUserRepository : IUserRepository
{
    private readonly WorkBoardDbContext _context;

    public EfUserRepository(WorkBoardDbContext context)
    {
        _context = context;
    }

    public async Task<User> AddAsync(User user, CancellationToken token)
    {
        await _context.Users.AddAsync(user, token);
        await _context.SaveChangesAsync(token);

        return user;
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken token)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        return user;
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken token)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        return user;
    }
}
