using MudBlazor;
using System.Threading.Tasks;

namespace SeaPizza.Client.Infrastructure.UserPreferences;

public interface IClientPreferenceManager : IPreferenceManager
{
    Task<MudTheme> GetCurrentThemeAsync();

    Task<bool> ToggleDarkModeAsync();

    Task<bool> ToggleDrawerAsync();
}
