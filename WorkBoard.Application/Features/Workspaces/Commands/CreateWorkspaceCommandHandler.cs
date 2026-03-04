using AutoMapper;
using MediatR;
using WorkBoard.Application.Features.DTOs;
using WorkBoard.Application.Interfaces;
using WorkBoard.Domain;

namespace WorkBoard.Application.Features.Workspaces.CreateWorkspace;

public sealed class CreateWorkspaceCommandHandler : IRequestHandler<CreateWorkspaceCommand, WorkspaceResponseDto>
{
    private readonly IWorkspaceRepository _repository;
    private readonly IMapper _mapper;

    public CreateWorkspaceCommandHandler(IWorkspaceRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<WorkspaceResponseDto> Handle(CreateWorkspaceCommand command, CancellationToken token)
    {
        var workspace = _mapper.Map<Workspace>(command.Request);

        await _repository.AddAsync(workspace, token);

        return _mapper.Map<WorkspaceResponseDto>(workspace);
    }
}
