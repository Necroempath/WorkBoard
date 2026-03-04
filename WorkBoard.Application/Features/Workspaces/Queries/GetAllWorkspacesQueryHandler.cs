using AutoMapper;
using MediatR;
using WorkBoard.Application.Features.DTOs;
using WorkBoard.Application.Interfaces;

namespace WorkBoard.Application.Features.Workspaces.Queries;

public sealed class GetAllWorkspacesQueryHandler : IRequestHandler<GetAllWorkspacesQuery, IEnumerable<WorkspaceResponseDto>>
{
    private readonly IWorkspaceRepository _repository;
    private readonly IMapper _mapper;

    public GetAllWorkspacesQueryHandler(IWorkspaceRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<WorkspaceResponseDto>> Handle(GetAllWorkspacesQuery query, CancellationToken token)
    {
        var workspaces = await _repository.GetAllAsync(token);

        return _mapper.Map<IEnumerable<WorkspaceResponseDto>>(workspaces);
    }
}
