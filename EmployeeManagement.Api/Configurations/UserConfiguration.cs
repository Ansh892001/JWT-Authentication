using EmployeeManagement.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeManagement.Api.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.Email)
               .IsRequired()
               .HasMaxLength(100);

        builder.HasIndex(x => x.Email)
               .IsUnique();

        builder.Property(x => x.Password)
               .IsRequired()
               .HasMaxLength(500);

        builder.Property(x => x.Role)
               .IsRequired()
               .HasMaxLength(50);
    }
}