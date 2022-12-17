using MudBlazor;
using System.Collections.Generic;

namespace SeaPizza.Client.Infrastructure.Theme;

public static class CustomColors
{
    public static readonly List<string> ThemeColors = new()
    {
        Light.Primary,
        Colors.Blue.Default,
        Colors.BlueGrey.Default,
        Colors.Purple.Default,
        Colors.Orange.Default,
        Colors.Red.Default,
        Colors.Amber.Default,
        Colors.DeepPurple.Default,
        Colors.Pink.Default,
        Colors.Indigo.Default,
        Colors.LightBlue.Default,
        Colors.Cyan.Default,
    };

    public static class Light
    {
        public const string Primary = "#009DEA";
        public const string Secondary = "#E73C7E";
        public const string Background = "#FFF";
        public const string AppbarBackground = "rgba(255,255,255,0.8)";
        public const string AppbarText = "#424242";
    }

    public static class Dark
    {
        public const string Primary = "#ff5c9b";
        public const string Secondary = "#38beff";
        public const string Background = "#1b1f22";
        public const string AppbarBackground = "#1b1f22";
        public const string DrawerBackground = "#121212";
        public const string Surface = "#202528";
        public const string Disabled = "#545454";
    }
}
