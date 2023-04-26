using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SeaPizza.Application.Common.Events;
using SeaPizza.Application.Common.Exceptions;
using SeaPizza.Application.Common.Interfaces;
using SeaPizza.Application.Identity.Roles;
using SeaPizza.Infrastructure.Persistence.Context;
using SeaPizza.Shared.Authorization;
using SeaPizza.Shared.Events;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SeaPizza.Infrastructure.Identity.Services;

internal class RoleService : IRoleService
{
    private readonly RoleManager<SeaPizzaRole> _roleManager;
    private readonly UserManager<SeaPizzaUser> _userManager;
    private readonly SeaPizzaDbContext _db;
    private readonly ICurrentUser _currentUser;

    public RoleService(
       RoleManager<SeaPizzaRole> roleManager,
       UserManager<SeaPizzaUser> userManager,
       SeaPizzaDbContext db,
       ICurrentUser currentUser)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _db = db;
        _currentUser = currentUser;
    }

    public async Task<string> CreateOrUpdateAsync(CreateOrUpdateRoleRequest request)
    {
        if (string.IsNullOrEmpty(request.Id))
        {
            // Create a new role.
            var role = new SeaPizzaRole(request.Name, request.Description);
            var result = await _roleManager.CreateAsync(role);

            if (!result.Succeeded)
            {
                throw new InternalServerException("Register role failed", result.GetErrors());
            }

            //await _events.PublishAsync(new ApplicationRoleCreatedEvent(role.Id, role.Name!));

            return string.Format($"Role {request.Name} Created.");
        }
        else
        {
            // Update an existing role.
            var role = await _roleManager.FindByIdAsync(request.Id);

            _ = role ?? throw new NotFoundException("Role Not Found");

            if (SeaPizzaRoles.IsDefault(role.Name!))
            {
                throw new ConflictException(string.Format($"Not allowed to modify {role.Name} Role."));
            }

            role.Name = request.Name;
            role.NormalizedName = request.Name.ToUpperInvariant();
            role.Description = request.Description;
            var result = await _roleManager.UpdateAsync(role);

            if (!result.Succeeded)
            {
                throw new InternalServerException("Update role failed", result.GetErrors());
            }

            //await _events.PublishAsync(new ApplicationRoleUpdatedEvent(role.Id, role.Name));

            return string.Format($"Role {role.Name} Updated.");
        }
    }

    public async Task<string> DeleteAsync(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);

        _ = role ?? throw new NotFoundException("Role Not Found");

        if (SeaPizzaRoles.IsDefault(role.Name!))
        {
            throw new ConflictException(string.Format($"Not allowed to delete {role.Name} Role."));
        }

        if ((await _userManager.GetUsersInRoleAsync(role.Name!)).Count > 0)
        {
            throw new ConflictException(string.Format($"Not allowed to delete {role.Name} Role as it is being used."));
        }

        await _roleManager.DeleteAsync(role);

        //await _events.PublishAsync(new ApplicationRoleDeletedEvent(role.Id, role.Name!));

        return string.Format($"Role {role.Name} Deleted.");
    }

    public async Task<bool> ExistsAsync(string roleName, string? excludeId) =>
        await _roleManager.FindByNameAsync(roleName)
            is SeaPizzaRole existingRole
            && existingRole.Id != excludeId;

    public async Task<RoleDto> GetByIdAsync(string id) =>
        await _db.Roles.SingleOrDefaultAsync(x => x.Id == id) is { } role
            ? role.Adapt<RoleDto>()
            : throw new NotFoundException("Role Not Found");

    public async Task<RoleDto> GetByIdWithPermissionsAsync(string roleId, CancellationToken cancellationToken)
    {
        var role = await GetByIdAsync(roleId);

        role.Permissions = await _db.RoleClaims
            .Where(c => c.RoleId == roleId && c.ClaimType == SeaPizzaClaims.Permission)
            .Select(c => c.ClaimValue!)
            .ToListAsync(cancellationToken);

        return role;
    }

    public async Task<int> GetCountAsync(CancellationToken cancellationToken) =>
        await _roleManager.Roles.CountAsync(cancellationToken);

    public async Task<List<RoleDto>> GetListAsync(CancellationToken cancellationToken) =>
        (await _roleManager.Roles.ToListAsync(cancellationToken))
            .Adapt<List<RoleDto>>();

    public async Task<string> UpdatePermissionsAsync(UpdateRolePermissionsRequest request, CancellationToken cancellationToken)
    {
        var role = await _roleManager.FindByIdAsync(request.RoleId);
        _ = role ?? throw new NotFoundException("Role Not Found");
        if (role.Name == SeaPizzaRoles.Admin)
        {
            throw new ConflictException("Not allowed to modify Permissions for this Role.");
        }

        var currentClaims = await _roleManager.GetClaimsAsync(role);

        // Remove permissions that were previously selected
        foreach (var claim in currentClaims.Where(c => !request.Permissions.Any(p => p == c.Value)))
        {
            var removeResult = await _roleManager.RemoveClaimAsync(role, claim);
            if (!removeResult.Succeeded)
            {
                throw new InternalServerException("Update permissions failed.", removeResult.GetErrors());
            }
        }

        // Add all permissions that were not previously selected
        foreach (string permission in request.Permissions.Where(c => !currentClaims.Any(p => p.Value == c)))
        {
            if (!string.IsNullOrEmpty(permission))
            {
                _db.RoleClaims.Add(new SeaPizzaRoleClaim
                {
                    RoleId = role.Id,
                    ClaimType = SeaPizzaClaims.Permission,
                    ClaimValue = permission,
                    CreatedBy = _currentUser.GetUserId().ToString()
                });
                await _db.SaveChangesAsync(cancellationToken);
            }
        }

        //await _events.PublishAsync(new ApplicationRoleUpdatedEvent(role.Id, role.Name!, true));

        return "Permissions Updated.";
    }
}
