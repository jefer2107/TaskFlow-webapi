using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntitiesConfiguration;

public class ChoreConfiguration

: IEntityTypeConfiguration<Chore>
{
    public void Configure(EntityTypeBuilder<Chore> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
        .IsRequired()
        .HasColumnType("varchar(30)");

        builder.Property(x => x.IsCompleted)
        .HasDefaultValue(false);

        builder.HasOne(x => x.User)
        .WithMany(x => x.Chores)
        .HasForeignKey(x => x.UserId)
        .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Category>()
        .WithMany(x => x.Chores)
        .HasForeignKey(x => x.CategoryId);


    }

}
