using AutoMapper;
using MediatR;
using WorkBoard.Application.Abstractions;
using WorkBoard.Application.Abstractions.Repositories;
using WorkBoard.Application.Common.Interfaces;
using WorkBoard.Application.Features.Workspaces.DTOs;
using WorkBoard.Domain;

namespace WorkBoard.Application.Features.Workspaces.CreateWorkspace;

public sealed class CreateWorkspaceCommandHandler : IRequestHandler<CreateWorkspaceCommand, WorkspaceResponseDto>
{
    private readonly IWorkspaceRepository _repository;
    private readonly ICurrentUserService _userService;
    private readonly IMapper _mapper;

    public CreateWorkspaceCommandHandler(IWorkspaceRepository repository, IMapper mapper, ICurrentUserService userService)
    {
        _repository = repository;
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<WorkspaceResponseDto> Handle(CreateWorkspaceCommand command, CancellationToken token)
    {
        var workspace = _mapper.Map<Workspace>(command.Request);
        
        await _repository.AddAsync(workspace, token);

        return _mapper.Map<WorkspaceResponseDto>(workspace);
    }
}
