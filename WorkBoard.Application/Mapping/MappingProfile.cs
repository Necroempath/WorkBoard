using AutoMapper;
using WorkBoard.Application.Features.Authentication.DTOs;
using WorkBoard.Application.Features.Workspaces.DTOs;
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
        CreateMap<User, AuthResponseDto>()
            .ForMember(dest => dest.Roles, 
            opt => opt.MapFrom(src => src.Roles.Select(r => r.Role.Name)));
    }
}
