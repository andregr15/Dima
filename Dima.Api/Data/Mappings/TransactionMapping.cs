using Dima.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mappings;

public class TransactionMapping : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transactions");

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Title)
            .HasColumnName("Title")
            .HasColumnType("NVARCHAR(80)")
            .IsRequired();

        builder
            .Property(x => x.CreatedAt)
            .IsRequired(true);

        builder
            .Property(x => x.PaidOrReceivedAt)
            .IsRequired(false);

        builder
            .Property(x => x.Type)
            .HasColumnName("Type")
            .HasColumnType("SMALLINT")
            .IsRequired();

        builder
            .Property(x => x.Amount)
            .HasColumnName("Amount")
            .HasColumnType("MONEY")
            .IsRequired();

        builder
            .Property(x => x.UserId)
            .HasColumnName("UserId")
            .HasColumnType("VARCHAR(160)")
            .IsRequired();
    }
}
