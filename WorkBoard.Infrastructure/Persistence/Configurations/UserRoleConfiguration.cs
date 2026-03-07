using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkBoard.Domain;

namespace WorkBoard.Infrastructure.Persistence.Configurations;

class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasKey(nameof(UserRole.UserId), nameof(UserRole.RoleId));
    }
}