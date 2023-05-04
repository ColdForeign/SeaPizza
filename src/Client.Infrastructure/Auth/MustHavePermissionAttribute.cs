using Microsoft.AspNetCore.Authorization;
using SeaPizza.Shared.Authorization;

public class MustHavePermissionAttribute : AuthorizeAttribute
{
    public MustHavePermissionAttribute(string action, string resource) =>
        Policy = SeaPizzaPermission.NameFor(action, resource);
}
