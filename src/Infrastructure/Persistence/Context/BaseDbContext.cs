using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SeaPizza.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeaPizza.Infrastructure.Persistence.Context;

public abstract class BaseDbContext : IdentityDbContext<SeaPizzaUser, SeaPizzaRole, string, IdentityUserClaim<string>, IdentityUserRole<string>, IdentityUserLogin<string>, SeaPizzaRoleClaim, IdentityUserToken<string>>
{
    private readonly DatabaseSettings _dbSettings;

    protected BaseDbContext(DbContextOptions options, IOptions<DatabaseSettings> dbSettings)
       : base(options)
    {
        _dbSettings = dbSettings.Value;
    }

    // Used by Dapper
    public IDbConnection Connection => Database.GetDbConnection();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // TODO: We want this only for development probably... maybe better make it configurable in logger.json config?
        optionsBuilder.EnableSensitiveDataLogging();

        optionsBuilder.UseNpgsql(_dbSettings.ConnectionString);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        int result = await base.SaveChangesAsync(cancellationToken);
        return result;
    }
}
