using Microsoft.EntityFrameworkCore;
using System;
using WorkBoard.Application.Interfaces;
using WorkBoard.Domain;

namespace WorkBoard.Infrastructure.Persistence;

public sealed class EfWorkspaceRepository : IWorkspaceRepository
{
    private readonly WorkBoardDbContext _context;

    public EfWorkspaceRepository(WorkBoardDbContext context)
    {
        _context = context;
    }

    public async Task<Workspace> AddAsync(Workspace workspace, CancellationToken cancellationToken)
    {
        await _context.Workspaces.AddAsync(workspace, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return workspace;
    }

    public async Task<IEnumerable<Workspace>> GetAllAsync(CancellationToken cancellationToken)
    {
        return _context.Workspaces;
    }

    public async Task<Workspace?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Workspaces
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
