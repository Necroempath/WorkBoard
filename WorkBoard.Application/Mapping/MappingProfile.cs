using AutoMapper;
using WorkBoard.Application.Features.Workspaces;
using WorkBoard.Domain;

namespace WorkBoard.Application.Mapping;

class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Workspace, WorkspaceResponseDto>();

        CreateMap<CreateWorkspaceDto, Workspace>();
    }
}
