using AutoMapper;
using WorkBoard.Application.Features.DTOs;
using WorkBoard.Domain;

namespace WorkBoard.Application.Mapping;

sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Workspace 
        CreateMap<Workspace, WorkspaceResponseDto>();
        CreateMap<CreateWorkspaceRequest, Workspace>();

        // Auth
        CreateMap<User, AuthResponseDto>();
    }
}
