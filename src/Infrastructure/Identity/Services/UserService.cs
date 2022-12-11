using SeaPizza.Application.Identity.Users;
using SeaPizza.Application.Identity.Users.Password;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace SeaPizza.Infrastructure.Identity.Services;

internal class UserService : IUserService
{
    public Task<string> AssignRolesAsync(string userId, UserRolesRequest request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }

    public Task ChangePasswordAsync(ChangePasswordRequest request, string userId)
    {
        throw new System.NotImplementedException();
    }

    public Task<string> ConfirmEmailAsync(string userId, string code, string tenant, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }

    public Task<string> ConfirmPhoneNumberAsync(string userId, string code)
    {
        throw new System.NotImplementedException();
    }

    public Task<string> CreateAsync(CreateUserRequest request, string origin)
    {
        throw new System.NotImplementedException();
    }

    public Task<bool> ExistsWithEmailAsync(string email, string? exceptId = null)
    {
        throw new System.NotImplementedException();
    }

    public Task<bool> ExistsWithNameAsync(string name)
    {
        throw new System.NotImplementedException();
    }

    public Task<bool> ExistsWithPhoneNumberAsync(string phoneNumber, string? exceptId = null)
    {
        throw new System.NotImplementedException();
    }

    public Task<string> ForgotPasswordAsync(ForgotPasswordRequest request, string origin)
    {
        throw new System.NotImplementedException();
    }

    public Task<UserDetailsDto> GetAsync(string userId, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }

    public Task<int> GetCountAsync(CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }

    public Task<List<UserDetailsDto>> GetListAsync(CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }

    public Task<string> GetOrCreateFromPrincipalAsync(ClaimsPrincipal principal)
    {
        throw new System.NotImplementedException();
    }

    public Task<List<string>> GetPermissionsAsync(string userId, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }

    public Task<List<UserRoleDto>> GetRolesAsync(string userId, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }

    public Task<bool> HasPermissionAsync(string userId, string permission, CancellationToken cancellationToken = default)
    {
        throw new System.NotImplementedException();
    }

    public Task InvalidatePermissionCacheAsync(string userId, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }

    public Task<string> ResetPasswordAsync(ResetPasswordRequest request)
    {
        throw new System.NotImplementedException();
    }

    public Task<PaginationResponse<UserDetailsDto>> SearchAsync(UserListFilter filter, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }

    public Task ToggleStatusAsync(ToggleUserStatusRequest request, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }

    public Task UpdateAsync(UpdateUserRequest request, string userId)
    {
        throw new System.NotImplementedException();
    }
}
