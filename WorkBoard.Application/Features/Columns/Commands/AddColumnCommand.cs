using MediatR;
using WorkBoard.Application.Behaviors;
using WorkBoard.Application.Features.Projects;

namespace WorkBoard.Application.Features.Columns.Commands;

public sealed record AddColumnCommand(string Name, Guid ProjectId) : IRequest<ProjectResponseDto>, IProjectRequest;