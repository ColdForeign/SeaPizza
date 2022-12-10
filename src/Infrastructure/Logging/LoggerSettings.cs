namespace SeaPizza.Infrastructure.Logging;

public class LoggerSettings
{
    public string AppName { get; set; } = "SeaPizza";
    public bool WriteToFile { get; set; } = false;
    public bool StructuredConsoleLogging { get; set; } = false;
    public string MinimumLogLevel { get; set; } = "Information";
}
