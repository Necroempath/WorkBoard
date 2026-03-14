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
            .ForMember(dest => dest.MemberId, opt => opt.MapFrom(src => src.MemberId))
            .ForMember(dest => dest.MemberName, opt => opt.MapFrom(src => src.Member.Name))
            .ForMember(dest => dest.MemberRole, opt => opt.MapFrom(src => src.Role))
            .ForMember(dest => dest.WorkspaceName, opt => opt.MapFrom(src => src.Workspace.Name))
            .ForMember(dest => dest.MemberEmail, opt => opt.MapFrom(src => src.Member.Email));

        // Auth
        CreateMap<User, AuthResponseDto>()
            .ForMember(dest => dest.Roles, 
            opt => opt.MapFrom(src => src.Roles.Select(r => r.Role.Name)));
    }
}
