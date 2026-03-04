using MediatR;
using WorkBoard.Application.Features.DTOs;

namespace WorkBoard.Application.Features.Workspaces.CreateWorkspace;

public sealed record CreateWorkspaceCommand(CreateWorkspaceRequest Request) : IRequest<WorkspaceResponseDto>;
