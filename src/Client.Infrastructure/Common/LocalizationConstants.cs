namespace SeaPizza.Client.Infrastructure.Common;

public record LanguageCode(string Code, string DisplayName, bool IsRTL = false);

public static class LocalizationConstants
{
    public static readonly LanguageCode[] SupportedLanguages =
    {
        new("uk-UA", "Українська"),
        new("en-US", "English")
    };
}
