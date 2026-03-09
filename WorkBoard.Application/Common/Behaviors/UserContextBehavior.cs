using MediatR;
using WorkBoard.Application.Abstractions;
using WorkBoard.Application.Common.Interfaces;

namespace WorkBoard.Application.Common.Behaviors;

public sealed class UserContextBehavior<IUserRequest, TResponse> : IPipelineBehavior<IUserRequest, TResponse> 
{
    private readonly ICurrentUserService _currentUser;

    public UserContextBehavior(ICurrentUserService currentService)
    {
        _currentUser = currentService;
    }

    public async Task<TResponse> Handle(IUserRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_currentUser.IsAuthenticated)
            throw new UnauthorizedAccessException("User not authenticated");

       request.UserId = _currentUser.UserId;

        return await next();
    }
}
