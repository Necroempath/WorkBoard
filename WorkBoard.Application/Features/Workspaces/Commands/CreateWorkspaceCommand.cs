using MediatR;
using WorkBoard.Application.Features.Workspaces.DTOs;

namespace WorkBoard.Application.Features.Workspaces.CreateWorkspace;

public sealed record CreateWorkspaceCommand(CreateWorkspaceRequest Request) : IRequest<WorkspaceResponseDto>;
