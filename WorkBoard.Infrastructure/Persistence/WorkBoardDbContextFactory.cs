//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;

//namespace WorkBoard.Infrastructure.Persistence;
//public sealed class WorkBoardDbContextFactory : IDesignTimeDbContextFactory<WorkBoardDbContext>
//{
//    public WorkBoardDbContext CreateDbContext(string[] args)
//    {
//        var optionsBuilder = new DbContextOptionsBuilder<WorkBoardDbContext>();

//        optionsBuilder.UseSqlServer(
//            "Server=localhost;Database=WorkBoardDb;Trusted_Connection=True;TrustServerCertificate=True");

//        return new WorkBoardDbContext(optionsBuilder.Options);
//    }
//}
