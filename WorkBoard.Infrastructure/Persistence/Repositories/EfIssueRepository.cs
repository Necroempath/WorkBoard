using Microsoft.EntityFrameworkCore;
using WorkBoard.Application.Abstractions.Repositories;
using WorkBoard.Domain.Entities;

namespace WorkBoard.Infrastructure.Persistence.Repositories;

public sealed class EfIssueRepository : IIssueRepository
{
    private readonly WorkBoardDbContext _context;

    public EfIssueRepository(WorkBoardDbContext context)
    {
        _context = context;
    }

    public async Task<Issue> CreateAsync(Issue issue, CancellationToken token)
    {
        await _context.Issues.AddAsync(issue, token);

        await _context.SaveChangesAsync(token);

        return issue;
    }

    public async Task DeleteAsync(Issue issue, CancellationToken token)
    {
        _context.Issues.Remove(issue);

        await _context.SaveChangesAsync(token);
    }

    public async Task<IEnumerable<Issue>> GetByProjectIdAsync(Guid projectId, CancellationToken token)
    {
        return await _context.Issues.Where(i => i.ProjectId == projectId).ToListAsync(token);
    }

    public async Task<IEnumerable<Issue>> GetByColumnIdAsync(Guid columnId, CancellationToken token)
    {
        return await _context.Issues.Where(i => i.ColumnId == columnId).ToListAsync(token);
    }

    public async Task<Issue?> GetByIdAsync(Guid issueId, CancellationToken token)
    {
        return await _context.Issues.FirstOrDefaultAsync(i => i.Id == issueId, token);
    }
}