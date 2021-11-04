using Microsoft.Extensions.Logging;

namespace SettingExtend.App
{
    public static class Log
    {
        public static ILoggerFactory Instance()
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
                    .AddConsole();
            });
            return loggerFactory;
        }
    }
}