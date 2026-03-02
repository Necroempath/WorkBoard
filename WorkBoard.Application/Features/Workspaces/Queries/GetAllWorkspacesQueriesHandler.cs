using AutoMapper;
using MediatR;
using WorkBoard.Application.Interfaces;

namespace WorkBoard.Application.Features.Workspaces.Queries;

public class GetAllWorkspacesQueriesHandler : IRequestHandler<GetAllWorkspacesQueries, IEnumerable<WorkspaceResponseDto>>
{
    private readonly IWorkspaceRepository _repository;
    private readonly IMapper _mapper;

    public GetAllWorkspacesQueriesHandler(IWorkspaceRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<WorkspaceResponseDto>> Handle(GetAllWorkspacesQueries request, CancellationToken token)
    {
        var workspaces = await _repository.GetAllAsync(token);

        return _mapper.Map<IEnumerable<WorkspaceResponseDto>>(workspaces);
    }
}
