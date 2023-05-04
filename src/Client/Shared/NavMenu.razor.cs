using SeaPizza.Client.Infrastructure.Auth;
using SeaPizza.Shared.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;

namespace SeaPizza.Client.Shared;

public partial class NavMenu
{
    [CascadingParameter]
    protected Task<AuthenticationState> AuthState { get; set; } = default!;

    [Inject]
    protected IAuthorizationService AuthService { get; set; } = default!;

    private bool _canViewDashboard;
    private bool _canViewRoles;
    private bool _canViewUsers;
    private bool _canViewProducts;

    private bool _canViewBrands;
    private bool CanViewAdministrationGroup => _canViewUsers || _canViewRoles;

    protected override async Task OnParametersSetAsync()
    {
        var user = (await AuthState).User;
        _canViewDashboard = await AuthService.HasPermissionAsync(user, SeaPizzaAction.View, SeaPizzaResource.Dashboard);
        _canViewRoles = await AuthService.HasPermissionAsync(user, SeaPizzaAction.View, SeaPizzaResource.Roles);
        _canViewUsers = await AuthService.HasPermissionAsync(user, SeaPizzaAction.View, SeaPizzaResource.Users);
        _canViewProducts = await AuthService.HasPermissionAsync(user, SeaPizzaAction.View, SeaPizzaResource.Products);
        _canViewBrands = await AuthService.HasPermissionAsync(user, SeaPizzaAction.View, SeaPizzaResource.Brands);
    }
}
