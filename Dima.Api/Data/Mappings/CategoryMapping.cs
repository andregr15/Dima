using Dima.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mappings;

public class CategoryMapping : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Title)
            .HasColumnName("Title")
            .HasColumnType("NVARCHAR(80)")
            .IsRequired();

        builder
            .Property(x => x.Description)
            .HasColumnName("Description")
            .HasColumnType("NVARCHAR(255)")
            .IsRequired(false);

        builder
            .Property(x => x.UserId)
            .HasColumnName("UserId")
            .HasColumnType("VARCHAR(160)")
            .IsRequired();
    }
}
