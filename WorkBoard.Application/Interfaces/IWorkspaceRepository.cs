using WorkBoard.Domain;

namespace WorkBoard.Application.Interfaces;

public interface IWorkspaceRepository
{
    Task<Workspace> AddAsync(Workspace workspace, CancellationToken token);
    Task<Workspace?> GetByIdAsync(Guid id, CancellationToken token);
    Task<IEnumerable<Workspace>> GetAllAsync(CancellationToken token);
}
