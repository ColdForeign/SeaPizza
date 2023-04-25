using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SeaPizza.Infrastructure.Identity;
using SeaPizza.Infrastructure.Persistence.Context;
using SeaPizza.Shared.Authorization;
using SeaPizza.Shared.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static SeaPizza.Shared.Constants.UserConstants;

namespace SeaPizza.Infrastructure.Persistence.Initialization;

internal class ApplicationDbSeeder
{
    private readonly RoleManager<SeaPizzaRole> _roleManager;
    private readonly UserManager<SeaPizzaUser> _userManager;
    private readonly CustomSeederRunner _seederRunner;
    private readonly ILogger<ApplicationDbSeeder> _logger;

    public ApplicationDbSeeder(RoleManager<SeaPizzaRole> roleManager, UserManager<SeaPizzaUser> userManager, CustomSeederRunner seederRunner, ILogger<ApplicationDbSeeder> logger)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _seederRunner = seederRunner;
        _logger = logger;
    }

    public async Task SeedDatabaseAsync(SeaPizzaDbContext dbContext, CancellationToken cancellationToken)
    {
        await SeedRolesAsync(dbContext);
        await SeedAdminUserAsync();
        await _seederRunner.RunSeedersAsync(cancellationToken);
    }

    private async Task SeedRolesAsync(SeaPizzaDbContext dbContext)
    {
        foreach (string roleName in SeaPizzaRoles.DefaultRoles)
        {
            if (await _roleManager.Roles.SingleOrDefaultAsync(r => r.Name == roleName)
                is not SeaPizzaRole role)
            {
                // Create the role
                _logger.LogInformation("Seeding {role} Role.", roleName);
                role = new SeaPizzaRole(roleName, $"{roleName} Role for root Tenant");
                await _roleManager.CreateAsync(role);
            }

            // Assign permissions
            if (roleName == SeaPizzaRoles.Basic)
            {
                await AssignPermissionsToRoleAsync(dbContext, SeaPizzaPermissions.Basic, role);
            }
            else if (roleName == SeaPizzaRoles.Admin)
            {
                await AssignPermissionsToRoleAsync(dbContext, SeaPizzaPermissions.Admin, role);
            }
        }
    }

    private async Task AssignPermissionsToRoleAsync(SeaPizzaDbContext dbContext, IReadOnlyList<SeaPizzaPermission> permissions, SeaPizzaRole role)
    {
        var currentClaims = await _roleManager.GetClaimsAsync(role);
        foreach (var permission in permissions)
        {
            if (!currentClaims.Any(c => c.Type == SeaPizzaClaims.Permission && c.Value == permission.Name))
            {
                _logger.LogInformation("Seeding {role} Permission '{permission}'.", role.Name, permission.Name);
                dbContext.RoleClaims.Add(new SeaPizzaRoleClaim
                {
                    RoleId = role.Id,
                    ClaimType = SeaPizzaClaims.Permission,
                    ClaimValue = permission.Name,
                    CreatedBy = "ApplicationDbSeeder"
                });
                await dbContext.SaveChangesAsync();
            }
        }
    }

    private async Task SeedAdminUserAsync()
    {
        if (await _userManager.Users.FirstOrDefaultAsync(u => u.Email == DefaultAdmin.EmailAddress)
            is not SeaPizzaUser adminUser)
        {
            string adminUserName = $"{DefaultAdmin.Name.Trim()}.{SeaPizzaRoles.Admin}".ToLowerInvariant();
            adminUser = new SeaPizzaUser
            {
                FirstName = DefaultAdmin.Name.ToLowerInvariant(),
                LastName = SeaPizzaRoles.Admin,
                Email = DefaultAdmin.EmailAddress,
                UserName = adminUserName,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                NormalizedEmail = DefaultAdmin.EmailAddress?.ToUpperInvariant(),
                NormalizedUserName = adminUserName.ToUpperInvariant(),
                IsActive = true
            };

            _logger.LogInformation("Seeding Default Admin User.");
            var password = new PasswordHasher<SeaPizzaUser>();
            adminUser.PasswordHash = password.HashPassword(adminUser, UserConstants.DefaultPassword);
            await _userManager.CreateAsync(adminUser);
        }

        // Assign role to user
        if (!await _userManager.IsInRoleAsync(adminUser, SeaPizzaRoles.Admin))
        {
            _logger.LogInformation("Assigning Admin Role to Admin User.");
            await _userManager.AddToRoleAsync(adminUser, SeaPizzaRoles.Admin);
        }
    }
}
