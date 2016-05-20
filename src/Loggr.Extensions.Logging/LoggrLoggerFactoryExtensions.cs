using System;
using Microsoft.Extensions.Logging;

namespace Loggr.Extensions.Logging
{
    public static class LoggrLoggerFactoryExtensions
    {
        public static ILoggerFactory AddLoggr( this ILoggerFactory factory, string logKey, string apiKey, string source = null )
        {
            return AddLoggr( factory, LogLevel.Information, logKey, apiKey, source );
        }

        public static ILoggerFactory AddLoggr( this ILoggerFactory factory, Func<string, LogLevel, EventId, bool> filter, string logKey, string apiKey, string source = null )
        {
            factory.AddProvider( new LoggrLoggerProvider( filter, logKey, apiKey, source ) );
            return factory;
        }

        public static ILoggerFactory AddLoggr( this ILoggerFactory factory, LogLevel minLevel, string logKey, string apiKey, string source = null )
        {
            return AddLoggr( factory, ( category, logLevel, eventId ) => logLevel >= minLevel, logKey, apiKey, source );
        }
    }
}
