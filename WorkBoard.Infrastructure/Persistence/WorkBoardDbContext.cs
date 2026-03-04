using Microsoft.EntityFrameworkCore;
using System;
using WorkBoard.Domain;

namespace WorkBoard.Infrastructure.Persistence;

public sealed class WorkBoardDbContext : DbContext
{
    public DbSet<Workspace> Workspaces => Set<Workspace>();
    public DbSet<User> Users => Set<User>();

    public WorkBoardDbContext(DbContextOptions<WorkBoardDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(WorkBoardDbContext).Assembly);
        base.OnModelCreating(builder);
    }
}
