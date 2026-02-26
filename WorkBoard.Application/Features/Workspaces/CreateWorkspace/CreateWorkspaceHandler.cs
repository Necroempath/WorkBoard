using WorkBoard.Application.Features.Workspaces;
using WorkBoard.Application.Interfaces;
using WorkBoard.Domain;

namespace WorkBoard.Application.Features.Workspaces.CreateWorkspace;

public sealed class CreateWorkspaceHandler
{
    private readonly IWorkspaceRepository _repository;

    public CreateWorkspaceHandler(IWorkspaceRepository repository)
    {
        _repository = repository;
    }

    public async Task<WorkspaceDto> Handle(CreateWorkspaceCommand command, CancellationToken cancellationToken)
    {
        var workspace = new Workspace(command.Name, command.OwnerId);

        await _repository.AddAsync(workspace, cancellationToken);

        return new WorkspaceDto(
            workspace.Id,
            workspace.Name,
            workspace.OwnerId
        );
    }
}
