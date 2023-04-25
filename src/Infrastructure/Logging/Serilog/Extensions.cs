using Microsoft.AspNetCore.Builder;
using Serilog.Events;
using Serilog;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Figgle;
using Serilog.Formatting.Compact;
using Serilog.Exceptions;

namespace SeaPizza.Infrastructure.Logging.Serilog;

public static class Extensions
{
    public static void RegisterSerilog(this WebApplicationBuilder builder)
    {
        builder.Services.AddOptions<LoggerSettings>().BindConfiguration(nameof(LoggerSettings));

        _ = builder.Host.UseSerilog((_, sp, serilogConfig) =>
        {
            var loggerSettings = sp.GetRequiredService<IOptions<LoggerSettings>>().Value;
            string appName = loggerSettings.AppName;
            bool writeToFile = loggerSettings.WriteToFile;
            bool structuredConsoleLogging = loggerSettings.StructuredConsoleLogging;
            string minLogLevel = loggerSettings.MinimumLogLevel;
            ConfigureEnrichers(serilogConfig, appName);
            ConfigureConsoleLogging(serilogConfig, structuredConsoleLogging);
            ConfigureWriteToFile(serilogConfig, writeToFile);
            SetMinimumLogLevel(serilogConfig, minLogLevel);
            OverideMinimumLogLevel(serilogConfig);
            Console.WriteLine(FiggleFonts.Standard.Render(loggerSettings.AppName));
        });
    }

    private static void ConfigureEnrichers(LoggerConfiguration serilogConfig, string appName)
    {
        serilogConfig
                        .Enrich.FromLogContext()
                        .Enrich.WithProperty("Application", appName)
                        .Enrich.WithExceptionDetails()
                        .Enrich.FromLogContext();
    }

    private static void ConfigureConsoleLogging(LoggerConfiguration serilogConfig, bool structuredConsoleLogging)
    {
        if (structuredConsoleLogging)
        {
            serilogConfig.WriteTo.Async(wt => wt.Console(new CompactJsonFormatter()));
        }
        else
        {
            serilogConfig.WriteTo.Async(wt => wt.Console());
        }
    }

    private static void ConfigureWriteToFile(LoggerConfiguration serilogConfig, bool writeToFile)
    {
        if (writeToFile)
        {
            serilogConfig.WriteTo.File(
             new CompactJsonFormatter(),
             "Logs/logs.json",
             restrictedToMinimumLevel: LogEventLevel.Information,
             rollingInterval: RollingInterval.Day,
             retainedFileCountLimit: 5);
        }
    }

    private static void OverideMinimumLogLevel(LoggerConfiguration serilogConfig)
    {
        serilogConfig
                     .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                     .MinimumLevel.Override("Hangfire", LogEventLevel.Warning)
                     .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                     .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Error);
    }

    private static void SetMinimumLogLevel(LoggerConfiguration serilogConfig, string minLogLevel)
    {
        switch (minLogLevel.ToLower())
        {
            case "debug":
                serilogConfig.MinimumLevel.Debug();
                break;
            case "information":
                serilogConfig.MinimumLevel.Information();
                break;
            case "warning":
                serilogConfig.MinimumLevel.Warning();
                break;
            default:
                serilogConfig.MinimumLevel.Information();
                break;
        }
    }
}
