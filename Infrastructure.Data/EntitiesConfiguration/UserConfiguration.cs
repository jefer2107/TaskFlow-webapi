using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntitiesConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
        .IsRequired()
        .HasColumnType("varchar(30)");

        builder.Property(x => x.Email)
        .IsRequired()
        .HasColumnType("varchar(30)");

        builder.Property(x => x.Password)
        .IsRequired()
        .HasColumnType("char(8)");
    }

}
