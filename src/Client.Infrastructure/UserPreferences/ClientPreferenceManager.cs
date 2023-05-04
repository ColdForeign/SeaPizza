using Blazored.LocalStorage;
using MudBlazor;
using SeaPizza.Client.Infrastructure.Common;
using SeaPizza.Client.Infrastructure.Theme;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SeaPizza.Client.Infrastructure.UserPreferences;

public class ClientPreferenceManager : IClientPreferenceManager
{
    private readonly ILocalStorageService _localStorageService;

    public ClientPreferenceManager(
        ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public async Task<bool> ToggleDarkModeAsync()
    {
        if (await GetPreference() is ClientPreference preference)
        {
            preference.IsDarkMode = !preference.IsDarkMode;
            await SetPreference(preference);
            return !preference.IsDarkMode;
        }

        return false;
    }

    public async Task<bool> ToggleDrawerAsync()
    {
        if (await GetPreference() is ClientPreference preference)
        {
            preference.IsDrawerOpen = !preference.IsDrawerOpen;
            await SetPreference(preference);
            return preference.IsDrawerOpen;
        }

        return false;
    }

    public async Task<bool> ChangeLanguageAsync(string languageCode)
    {
        if (await GetPreference() is ClientPreference preference)
        {
            var language = Array.Find(LocalizationConstants.SupportedLanguages, a => a.Code == languageCode);
            if (language?.Code is not null)
            {
                preference.LanguageCode = language.Code;
            }
            else
            {
                preference.LanguageCode = "en-EN";
            }

            await SetPreference(preference);
            return true;
        }

        return false;
    }

    public async Task<MudTheme> GetCurrentThemeAsync()
    {
        if (await GetPreference() is ClientPreference preference)
        {
            if (preference.IsDarkMode) return new DarkTheme();
        }

        return new LightTheme();
    }

    public async Task<string> GetPrimaryColorAsync()
    {
        if (await GetPreference() is ClientPreference preference)
        {
            string colorCode = preference.PrimaryColor;
            if (Regex.Match(colorCode, "^#(?:[0-9a-fA-F]{3,4}){1,2}$").Success)
            {
                return colorCode;
            }
            else
            {
                preference.PrimaryColor = CustomColors.Light.Primary;
                await SetPreference(preference);
                return preference.PrimaryColor;
            }
        }

        return CustomColors.Light.Primary;
    }

    public async Task<bool> IsDrawerOpen()
    {
        if (await GetPreference() is ClientPreference preference)
        {
            return preference.IsDrawerOpen;
        }

        return false;
    }

    public static string Preference = "clientPreference";

    public async Task<IPreference> GetPreference()
    {
        return await _localStorageService.GetItemAsync<ClientPreference>(Preference) ?? new ClientPreference();
    }

    public async Task SetPreference(IPreference preference)
    {
        await _localStorageService.SetItemAsync(Preference, preference as ClientPreference);
    }

}
