using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Formatting.Json;

namespace GatewaySolution.Logging
{
    public static class Logging
    {
        public static Action<HostBuilderContext, LoggerConfiguration> ConfigureLogger =>
            (context, loggerConfiguration) =>
            {

                var env = context.HostingEnvironment;
                var config = context.Configuration.GetSection("Logging");

                var defaultLogLevel = config["LogLevel:Default"] ?? "Information";
                var microsoftAspNetCoreLogLevel = config["LogLevel:Microsoft.AspNetCore"] ?? "Warning";


                var filterConfig = context.Configuration.GetSection("Logging:FilterByLevel");
                var filterByError = filterConfig.GetValue<bool>("Error");
                var filterByInformation = filterConfig.GetValue<bool>("Information");
                var filterByWarning = filterConfig.GetValue<bool>("Warning");

                loggerConfiguration.MinimumLevel.ControlledBy(new LoggingLevelSwitch());
                loggerConfiguration.MinimumLevel.Override("Microsoft.AspNetCore", ParseLogLevel(microsoftAspNetCoreLogLevel));
                loggerConfiguration
                    .Enrich.FromLogContext()
                    .Enrich.WithProperty("ApplicationName", env.ApplicationName)
                    .Enrich.WithProperty("EnvironmentName", env.EnvironmentName)
                    .Enrich.WithExceptionDetails()
                    .WriteTo.Console()
                    .WriteTo.Logger(lc => lc
                        .Filter.ByIncludingOnly(e => (filterByError && e.Level == LogEventLevel.Error) ||
                                                (filterByInformation && e.Level == LogEventLevel.Information) ||
                                                (filterByWarning && e.Level == LogEventLevel.Warning))
                        .WriteTo.File(
                            new JsonFormatter(renderMessage: true),
                           GetLogFilePath(env),
                            fileSizeLimitBytes: null,
                            rollOnFileSizeLimit: true,
                            retainedFileCountLimit: 30));

                if (context.HostingEnvironment.IsDevelopment())
                {
                      loggerConfiguration.MinimumLevel.Override("Discount", LogEventLevel.Debug);
                }
            };
        private static string GetLogFilePath(IHostEnvironment hostEnvironment)
        {
            return $"{hostEnvironment.ContentRootPath}/logs/log{DateTime.Now:yyyyMMdd}.txt";
        }
        private static LogEventLevel ParseLogLevel(string level)
        {
            if (Enum.TryParse<LogEventLevel>(level, out var logLevel))
            {
                return logLevel;
            }

            return LogEventLevel.Information;
        }
    }

}
