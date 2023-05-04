using Microsoft.AspNetCore.Authorization;
using SeaPizza.Shared.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SeaPizza.Client.Infrastructure.Auth;

public static class AuthorizationServiceExtensions
{
    public static async Task<bool> HasPermissionAsync(this IAuthorizationService service, ClaimsPrincipal user, string action, string resource) =>
        (await service.AuthorizeAsync(user, null, SeaPizzaPermission.NameFor(action, resource))).Succeeded;
}
