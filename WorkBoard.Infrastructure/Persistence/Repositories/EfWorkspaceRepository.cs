using Microsoft.EntityFrameworkCore;
using System;
using WorkBoard.Application.Abstractions.Repositories;
using WorkBoard.Domain;

namespace WorkBoard.Infrastructure.Persistence.Repositories;

public sealed class EfWorkspaceRepository : IWorkspaceRepository
{
    private readonly WorkBoardDbContext _context;

    public EfWorkspaceRepository(WorkBoardDbContext context)
    {
        _context = context;
    }

    public async Task<Workspace> AddAsync(Workspace workspace, CancellationToken token)
    {
        await _context.Workspaces.AddAsync(workspace, token);
        await _context.SaveChangesAsync(token);

        return workspace;
    }

    public async Task<IEnumerable<Workspace>> GetAllAsync(CancellationToken token)
    {
        return _context.Workspaces;
    }

    public async Task<Workspace?> GetByIdAsync(Guid id, CancellationToken token)
    {
        return await _context.Workspaces
            .FirstOrDefaultAsync(x => x.Id == id, token);
    }
}
