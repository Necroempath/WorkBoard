using AutoMapper;
using WorkBoard.Application.Features.Authentication;
using WorkBoard.Application.Features.WorkspaceMemberships;
using WorkBoard.Application.Features.Workspaces;
using WorkBoard.Domain.Entities;

namespace WorkBoard.Application.Mapping;

sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Workspace 
        CreateMap<Workspace, WorkspaceResponseDto>();
        CreateMap<CreateWorkspaceRequest, Workspace>();

        // WorkspaceMembership
        CreateMap<WorkspaceMembership, WorkspaceMembershipResponseDto>()
            .ForMember(dest => dest.MemberId, opt => opt.MapFrom(src => src.User.Id))
            .ForMember(dest => dest.MemberName, opt => opt.MapFrom(src => src.User.Name))
            .ForMember(dest => dest.MemberEmail, opt => opt.MapFrom(src => src.User.Email));

        // Auth
        CreateMap<User, AuthResponseDto>()
            .ForMember(dest => dest.Roles, 
            opt => opt.MapFrom(src => src.Roles.Select(r => r.Role.Name)));
    }
}
