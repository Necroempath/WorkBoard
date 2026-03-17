using Microsoft.EntityFrameworkCore;
using WorkBoard.Application.Abstractions;
using WorkBoard.Infrastructure.Persistence;
using WorkBoard.Application.Abstractions.Repositories;
using WorkBoard.Infrastructure.Persistence.Repositories;
using WorkBoard.Infrastructure.Implementations;

namespace WorkBoard.API.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WorkBoardDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Default")));

        services.AddScoped<IWorkspaceRepository, EfWorkspaceRepository>();
        services.AddScoped<IProjectRepository, EfProjectRepository>();
        services.AddScoped<IUserRepository, EfUserRepository>();
        services.AddScoped<IRoleRepository, EfRoleRepository>();
        services.AddScoped<IWorkspaceMembershipRepository, EfWorkspaceMembershipRepository>();
        services.AddScoped<IRefreshTokenRepository, EfRefreshTokenRepository>();

        services.AddScoped<IRefreshTokenGenerator, RefreshTokenGenerator>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();

        return services;
    }
}