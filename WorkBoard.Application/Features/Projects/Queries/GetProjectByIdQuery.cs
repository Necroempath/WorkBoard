using MediatR;
using WorkBoard.Application.Behaviors;

namespace WorkBoard.Application.Features.Projects.Queries;

public sealed record GetProjectByIdQuery(Guid ProjectId, Guid WorkspaceId) : IRequest<ProjectResponseDto?>, IWorkspaceRequest;
