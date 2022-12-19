using Microsoft.AspNetCore.Components;
using MudBlazor;
using SeaPizza.Client.Infrastructure.UserPreferences;
using static MudBlazor.CategoryTypes;

namespace SeaPizza.Client.Shared;

public partial class MainLayout
{
    [Parameter]
    public RenderFragment ChildContent { get; set; } = default!;
    [Parameter]
    public EventCallback OnDarkModeToggle { get; set; }
    [Parameter]
    public EventCallback<bool> OnRightToLeftToggle { get; set; }

    private bool _drawerOpen;

    protected override async Task OnInitializedAsync()
    {
        if (await ClientPreferences.GetPreference() is ClientPreference preference)
        {
            _drawerOpen = preference.IsDrawerOpen;
        }
    }

    public async Task ToggleDarkMode()
    {
        await OnDarkModeToggle.InvokeAsync();
    }

    private async Task DrawerToggle()
    {
        _drawerOpen = await ClientPreferences.ToggleDrawerAsync();
    }

    private void Logout()
    {
       
    }

    private void Profile()
    {
        Navigation.NavigateTo("/account");
    }
}
