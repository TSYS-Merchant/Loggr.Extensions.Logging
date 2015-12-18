using System;
using Microsoft.Extensions.Logging;

namespace Loggr.Extensions.Logging
{
    public class LoggrLoggerProvider : ILoggerProvider
    {
        private readonly Func<string, LogLevel, int, bool> m_filter;
        private readonly LogClient m_logClient;
        private readonly string m_source;

        public LoggrLoggerProvider( Func<string, LogLevel, int, bool> filter, string source = null )
        {
            m_filter = filter;
            m_logClient = new LogClient();
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
