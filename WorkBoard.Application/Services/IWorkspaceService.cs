using WorkBoard.Application.Features.Workspaces.CreateWorkspace;
using WorkBoard.Application.Interfaces;
using WorkBoard.Domain;

namespace WorkBoard.Application.Services;

public class WorkspaceService
{
    private readonly IWorkspaceRepository _repository;

    public WorkspaceService(IWorkspaceRepository repository)
    {
        _repository = repository;
    }

    public async Task<Workspace> CreateAsync(CreateWorkspaceCommand command, CancellationToken cancellationToken)
    {
        var workspace = new Workspace(command.Name, command.OwnerId);

        return await _repository.AddAsync(workspace, cancellationToken);
    }

    public Task<Workspace?> GetByIdAsync(Guid id, CancellationToken cancellationToken) => _repository.GetByIdAsync(id, cancellationToken);

    public Task<IEnumerable<Workspace>> GetAllAsync(CancellationToken cancellationToken) => _repository.GetAllAsync(cancellationToken);
}