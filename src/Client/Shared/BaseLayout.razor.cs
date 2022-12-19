using MudBlazor;
using SeaPizza.Client.Infrastructure.Theme;
using SeaPizza.Client.Infrastructure.UserPreferences;

namespace SeaPizza.Client.Shared;

public partial class BaseLayout
{
    private ClientPreference? _themePreference;
    private MudTheme _currentTheme = new LightTheme();
    private bool _themeDrawerOpen;

    protected override async Task OnInitializedAsync()
    {
        _themePreference = await ClientPreferences.GetPreference() as ClientPreference;
        if (_themePreference == null) _themePreference = new ClientPreference();
        SetCurrentTheme(_themePreference);
    }

    private async Task ThemePreferenceChanged(ClientPreference themePreference)
    {
        SetCurrentTheme(themePreference);
        await ClientPreferences.SetPreference(themePreference);
    }

    private void SetCurrentTheme(ClientPreference themePreference)
    {
        _currentTheme = themePreference.IsDarkMode ? new DarkTheme() : new LightTheme();
        _currentTheme.Palette.Primary = themePreference.PrimaryColor;
        _currentTheme.Palette.Secondary = themePreference.SecondaryColor;
        _currentTheme.LayoutProperties.DefaultBorderRadius = $"{themePreference.BorderRadius}px";
        _currentTheme.LayoutProperties.DefaultBorderRadius = $"{themePreference.BorderRadius}px";
    }
}
