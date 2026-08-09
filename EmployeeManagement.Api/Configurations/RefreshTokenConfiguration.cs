using EmployeeManagement.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RefreshTokenConfiguration
    : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.Property(x => x.Token)
               .IsRequired();

        builder.HasIndex(x => x.Token)
               .IsUnique();

        builder.HasOne(x => x.User)
               .WithMany(x => x.RefreshTokens)
               .HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}