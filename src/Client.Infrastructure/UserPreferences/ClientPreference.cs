﻿using SeaPizza.Client.Infrastructure.Common;
using SeaPizza.Client.Infrastructure.Theme;
using System.Linq;

namespace SeaPizza.Client.Infrastructure.UserPreferences;

public class ClientPreference : IPreference
{
    public bool IsDarkMode { get; set; }

    //public bool IsRTL { get; set; }

    public bool IsDrawerOpen { get; set; }

    public string PrimaryColor { get; set; } = CustomColors.Light.Primary;

    public string SecondaryColor { get; set; } = CustomColors.Light.Secondary;

    public double BorderRadius { get; set; } = 5;

    public string LanguageCode { get; set; } = LocalizationConstants.SupportedLanguages.FirstOrDefault()?.Code ?? "en-US";

    public TablePreference TablePreference { get; set; } = new TablePreference();
}
