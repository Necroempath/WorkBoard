using WorkBoard.Domain;

namespace WorkBoard.Application.Interfaces;

public interface IWorkspaceRepository
{
    Task<Workspace> AddAsync(Workspace workspace, CancellationToken cancellationToken);
    Task<Workspace?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Workspace>> GetAllAsync(CancellationToken cancellationToken);
}
