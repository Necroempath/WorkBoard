using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkBoard.Application.Entities;

namespace WorkBoard.Infrastructure.Persistence.Configurations;

class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{

    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens");

        builder.HasKey(x => x.Token);

        builder.ToTable(t => t.HasCheckConstraint(
            "CK_RefreshTokens_ExpireAt",
            "ExpireAt > GETDATE()"
        ));

        builder.HasOne(rt => rt.User)
               .WithMany() 
               .HasForeignKey(rt => rt.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
