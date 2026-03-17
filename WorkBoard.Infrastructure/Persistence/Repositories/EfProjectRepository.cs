using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using WorkBoard.Application.Abstractions.Repositories;
using WorkBoard.Domain.Entities;

namespace WorkBoard.Infrastructure.Persistence.Repositories;

public sealed class EfProjectRepository : IProjectRepository
{
    private readonly WorkBoardDbContext _context;

    public EfProjectRepository(WorkBoardDbContext context)
    {
        _context = context;
    }

    public async Task<Project> CreateAsync(Project project, CancellationToken token)
    {
        await _context.Projects.AddAsync(project, token);

        await _context.SaveChangesAsync(token);

        return project;
    }

    public async Task DeleteAsync(Guid projectId, CancellationToken token)
    {
        var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == projectId, token);

        if (project is null) return;

        _context.Remove(project);

        await _context.SaveChangesAsync(token);
    }

    public async Task<IEnumerable<Project>> GetByWorkspaceIdAsync(Guid workspaceId, CancellationToken token)
    {
        return await _context.Projects.Where(p => p.WorkspaceId == workspaceId).ToListAsync(token);
    }

    public async Task<Project?> GetByIdAsync(Guid projectId, CancellationToken token)
    {
        return await _context.Projects.FirstOrDefaultAsync(p => p.Id == projectId, token);
    }

    public async Task SaveAsync(CancellationToken token)
    {
        await _context.SaveChangesAsync(token);
    }
}