using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mappings.Identity;

public class IdentityRoleClaimMapping : IEntityTypeConfiguration<IdentityRoleClaim<long>>
{
    public void Configure(EntityTypeBuilder<IdentityRoleClaim<long>> builder)
    {
        builder.ToTable("IdentityRoleClaims");
        builder.HasKey(uc => uc.Id);
        builder.Property(uc => uc.ClaimType).HasMaxLength(255);
        builder.Property(uc => uc.ClaimValue).HasMaxLength(255);
    }
}
