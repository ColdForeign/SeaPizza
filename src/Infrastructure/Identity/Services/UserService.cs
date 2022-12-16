using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using SeaPizza.Application.Common.Caching;
using SeaPizza.Application.Common.Events;
using SeaPizza.Application.Common.Exceptions;
using SeaPizza.Application.Common.FileStorage;
using SeaPizza.Application.Common.Mailing;
using SeaPizza.Application.Identity.Users;
using SeaPizza.Application.Identity.Users.Password;
using SeaPizza.Infrastructure.Auth;
using SeaPizza.Infrastructure.Persistence.Context;
using SeaPizza.Shared.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace SeaPizza.Infrastructure.Identity.Services;

internal partial class UserService : IUserService
{
    private readonly SignInManager<SeaPizzaUser> _signInManager;
    private readonly UserManager<SeaPizzaUser> _userManager;
    private readonly RoleManager<SeaPizzaRole> _roleManager;
    private readonly SeaPizzaDbContext _db;
    private readonly SecuritySettings _securitySettings;
    private readonly IFileStorageService _fileStorage;
    private readonly ICacheService _cache;
    private readonly ICacheKeyService _cacheKeys;

    public UserService(
        SignInManager<SeaPizzaUser> signInManager,
        UserManager<SeaPizzaUser> userManager,
        RoleManager<SeaPizzaRole> roleManager,
        SeaPizzaDbContext db,
        IFileStorageService fileStorage,
        ICacheService cache,
        ICacheKeyService cacheKeys,
        IOptions<SecuritySettings> securitySettings)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
        _db = db;
        _fileStorage = fileStorage;
        _cache = cache;
        _cacheKeys = cacheKeys;
        _securitySettings = securitySettings.Value;
    }

    public async Task<bool> ExistsWithNameAsync(string name)
    {
        return await _userManager.FindByNameAsync(name) is not null;
    }

    public async Task<bool> ExistsWithEmailAsync(string email, string? exceptId = null)
    {
        return await _userManager.FindByEmailAsync(email.Normalize()) is SeaPizzaUser user && user.Id != exceptId;
    }

    public async Task<bool> ExistsWithPhoneNumberAsync(string phoneNumber, string? exceptId = null)
    {
        return await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber) is SeaPizzaUser user && user.Id != exceptId;
    }

    public async Task<List<UserDetailsDto>> GetListAsync(CancellationToken cancellationToken) =>
        (await _userManager.Users
                .AsNoTracking()
                .ToListAsync(cancellationToken))
            .Adapt<List<UserDetailsDto>>();

    public Task<int> GetCountAsync(CancellationToken cancellationToken) =>
        _userManager.Users.AsNoTracking().CountAsync(cancellationToken);

    public async Task<UserDetailsDto> GetAsync(string userId, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users
            .AsNoTracking()
            .Where(u => u.Id == userId)
            .FirstOrDefaultAsync(cancellationToken);

        _ = user ?? throw new NotFoundException("User Not Found.");

        return user.Adapt<UserDetailsDto>();
    }

    public async Task ToggleStatusAsync(ToggleUserStatusRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.Where(u => u.Id == request.UserId).FirstOrDefaultAsync(cancellationToken);

        _ = user ?? throw new NotFoundException("User Not Found.");

        bool isAdmin = await _userManager.IsInRoleAsync(user, SeaPizzaRoles.Admin);
        if (isAdmin)
        {
            throw new ConflictException("Administrators Profile's Status cannot be toggled");
        }

        user.IsActive = request.ActivateUser;

        await _userManager.UpdateAsync(user);

        //await _events.PublishAsync(new SeaPizzaUserUpdatedEvent(user.Id));
    }
}
