using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SeaPizza.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeaPizza.Infrastructure.Persistence.Context;

public abstract class BaseDbContext : IdentityDbContext<SeaPizzaUser, SeaPizzaRole, string, IdentityUserClaim<string>, IdentityUserRole<string>, IdentityUserLogin<string>, SeaPizzaRoleClaim, IdentityUserToken<string>>
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        int result = await base.SaveChangesAsync(cancellationToken);
        return result;
    }
}
