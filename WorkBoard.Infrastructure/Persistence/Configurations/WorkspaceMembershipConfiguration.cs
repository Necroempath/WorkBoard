using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkBoard.Domain.Entities;

namespace WorkBoard.Infrastructure.Persistence.Configurations;

class WorkspaceMembershipConfiguration : IEntityTypeConfiguration<WorkspaceMembership>
{

    public void Configure(EntityTypeBuilder<WorkspaceMembership> builder)
    {
        builder.ToTable("WorkspaceMemberships");

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => new { x.WorkspaceId, x.UserId })
                .IsUnique();

        builder.HasIndex(x => x.WorkspaceId)
               .IsUnique()
               .HasFilter("[Role] = 0");
    }
}
