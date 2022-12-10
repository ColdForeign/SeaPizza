using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeaPizza.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace SeaPizza.Infrastructure.Persistence.Configuration;

public class SeaPizzaUserConfig : IEntityTypeConfiguration<SeaPizzaUser>
{
    public void Configure(EntityTypeBuilder<SeaPizzaUser> builder)
    {
        builder.ToTable("Users", SchemaNames.Identity);
    }
}

public class SeaPizzaRoleConfig : IEntityTypeConfiguration<SeaPizzaRole>
{
    public void Configure(EntityTypeBuilder<SeaPizzaRole> builder) =>
        builder.ToTable("Roles", SchemaNames.Identity);
}

public class SeaPizzaRoleClaimConfig : IEntityTypeConfiguration<SeaPizzaRoleClaim>
{
    public void Configure(EntityTypeBuilder<SeaPizzaRoleClaim> builder) =>
        builder.ToTable("RoleClaims", SchemaNames.Identity);
}

public class IdentityUserRoleConfig : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder) =>
        builder.ToTable("UserRoles", SchemaNames.Identity);
}

public class IdentityUserClaimConfig : IEntityTypeConfiguration<IdentityUserClaim<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserClaim<string>> builder) =>
        builder.ToTable("UserClaims", SchemaNames.Identity);
}

public class IdentityUserLoginConfig : IEntityTypeConfiguration<IdentityUserLogin<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserLogin<string>> builder) =>
        builder.ToTable("UserLogins", SchemaNames.Identity);
}

public class IdentityUserTokenConfig : IEntityTypeConfiguration<IdentityUserToken<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserToken<string>> builder) =>
        builder.ToTable("UserTokens", SchemaNames.Identity);
}