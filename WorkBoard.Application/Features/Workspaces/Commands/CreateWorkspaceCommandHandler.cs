using AutoMapper;
using MediatR;
using WorkBoard.Application.Abstractions;
using WorkBoard.Application.Abstractions.Repositories;
using WorkBoard.Domain;

namespace WorkBoard.Application.Features.Workspaces.CreateWorkspace;

public sealed class CreateWorkspaceCommandHandler : IRequestHandler<CreateWorkspaceCommand, WorkspaceResponseDto>
{
    private readonly IWorkspaceRepository _repository;
    private readonly ICurrentUserService _currentUser;
    private readonly IMapper _mapper;

    public CreateWorkspaceCommandHandler(IWorkspaceRepository repository, IMapper mapper, ICurrentUserService currentUser)
    {
        _repository = repository;
        _currentUser = currentUser;
        _mapper = mapper;
    }

    public async Task<WorkspaceResponseDto> Handle(CreateWorkspaceCommand command, CancellationToken token)
    {
        var workspace = _mapper.Map<Workspace>(command.Request);

        if (!_currentUser.IsAuthenticated)
            throw new UnauthorizedAccessException("User not authenticated");

        workspace.SetOwner(new Guid(_currentUser.UserId!));

        await _repository.AddAsync(workspace, token);

        return _mapper.Map<WorkspaceResponseDto>(workspace);
    }
}
