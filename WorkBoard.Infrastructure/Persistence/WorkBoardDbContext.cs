using Microsoft.EntityFrameworkCore;
using WorkBoard.Domain.Entities;

namespace WorkBoard.Infrastructure.Persistence;

public sealed class WorkBoardDbContext : DbContext
{
    public DbSet<Workspace> Workspaces => Set<Workspace>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<WorkspaceMembership> WorkspaceMemberships => Set<WorkspaceMembership>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    public WorkBoardDbContext(DbContextOptions<WorkBoardDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(WorkBoardDbContext).Assembly);
        base.OnModelCreating(builder);
    }
}
