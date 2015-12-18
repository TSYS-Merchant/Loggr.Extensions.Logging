using System;
using Microsoft.Extensions.Logging;

namespace Loggr.Extensions.Logging
{
    public static class LoggrLoggerFactoryExtensions
    {
        public static ILoggerFactory AddLoggr( this ILoggerFactory factory, string source = null )
        {
            return AddLoggr( factory, LogLevel.Information, source );
        }

        public static ILoggerFactory AddLoggr( this ILoggerFactory factory, Func<string, LogLevel, int, bool> filter, string source = null )
        {
            factory.AddProvider( new LoggrLoggerProvider( filter, source ) );
            return factory;
        }

        public static ILoggerFactory AddLoggr( this ILoggerFactory factory, LogLevel minLevel, string source = null )
        {
            return AddLoggr( factory, ( category, logLevel, eventId ) => logLevel >= minLevel, source );
        }
    }
}
