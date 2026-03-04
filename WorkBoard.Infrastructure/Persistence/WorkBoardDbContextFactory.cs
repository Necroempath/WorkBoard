using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WorkBoard.Infrastructure.Persistence;
public sealed class WorkBoardDbContextFactory : IDesignTimeDbContextFactory<WorkBoardDbContext>
{
    public WorkBoardDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<WorkBoardDbContext>();

        var connectionString = Environment.GetEnvironmentVariable("WORKBOARD_DB");

        optionsBuilder.UseSqlServer(connectionString);

        return new WorkBoardDbContext(optionsBuilder.Options);
    }
}
