using System;
using Microsoft.Extensions.Logging;

namespace Loggr.Extensions.Logging
{
    public class LoggrLoggerProvider : ILoggerProvider
    {
        private readonly Func<string, LogLevel, EventId, bool> m_filter;
        private readonly LogClient m_logClient;
        private readonly string m_source;

        public LoggrLoggerProvider( Func<string, LogLevel, EventId, bool> filter, string logKey, string apiKey, string source = null )
        {
            m_filter = filter;
            m_logClient = new LogClient( logKey, apiKey );
            m_source = source;
        }

        public ILogger CreateLogger( string name )
        {
            return new LoggrLogger( m_logClient, name, m_source, m_filter );
        }

        public void Dispose()
        {
        }
    }
}
