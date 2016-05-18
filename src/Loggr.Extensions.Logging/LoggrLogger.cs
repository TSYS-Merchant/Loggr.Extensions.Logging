using System;
using Microsoft.Extensions.Logging;

namespace Loggr.Extensions.Logging
{
    public class LoggrLogger : ILogger
    {
        private readonly Func<string, LogLevel, EventId, bool> m_filter;
        private readonly string m_name;
        private readonly LogClient m_client;
        private readonly string m_source;

        public LoggrLogger( LogClient client, string name, string source )
            : this( client, name, source, null )
        { }

        public LoggrLogger( LogClient client, string name, string source, Func<string, LogLevel, EventId, bool> filter )
        {
            m_client = client;
            m_name = name;
            m_source = source;
            m_filter = filter;
        }

        public IDisposable BeginScope<TState>( TState state )
        {
            return null;
        }

        public bool IsEnabled( LogLevel logLevel )
        {
            return true;
        }

        public bool IsEnabled( LogLevel logLevel, EventId eventId )
        {
            return m_filter == null || m_filter( m_name, logLevel, eventId );
        }

        public void Log<TState>( LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter )
        {
            if( !IsEnabled( logLevel, eventId ) )
            {
                return;
            }

            if( state == null && exception == null )
            {
                return;
            }

            if( formatter == null )
            {
                throw new ArgumentNullException( nameof( formatter ) );
            }

            var message = formatter( state, exception );

            if( string.IsNullOrWhiteSpace( message ) )
            {
                message = string.Empty;
            }

            // Exception message or take the first 100 characters of the message as the loggr text
            var text = exception?.Message ?? message;
            if( text.Length > 100 )
            {
                text = string.Concat( text.Trim().Substring( 0, 97 ), "..." );
            }

            var logEvent = Events.Create()
                .UseLogClient( m_client )
                .Text( text )
                .Tags( new[] { GetLogLevel( logLevel ), eventId.ToString() } )
                .Source( m_source ?? m_name )
                .Timestamp( DateTime.UtcNow )
                .DataType( DataType.html );

            if( exception != null && logLevel == LogLevel.Error )
            {
                FormatExceptionMessage( logEvent, message );
            }
            else
            {
                logEvent.AddData( message );
            }

            // add the exception
            if( exception != null )
            {
                logEvent.AddData( $"<br />{Utility.ExceptionFormatter.Format( exception )}" );
            }

            // post the event asynchronously
            logEvent.Post( true );
        }

        private string GetLogLevel( LogLevel logLevel )
        {
            switch( logLevel )
            {
                case LogLevel.Trace:
                    return "trace";
                case LogLevel.Debug:
                    return "debug";
                case LogLevel.Information:
                    return "info";
                case LogLevel.Warning:
                    return "warning";
                case LogLevel.Error:
                    return "error";
                case LogLevel.Critical:
                    return "critical";
            }

            return "info";
        }

        private void FormatExceptionMessage( FluentEvent logEvent, string message )
        {
            var dataItems = message.Split( new[] { " | " }, StringSplitOptions.None );
            foreach( var item in dataItems )
            {
                var itemKvp = item.Split( new[] { ":" }, 2, StringSplitOptions.None );
                logEvent.AddData( itemKvp.Length > 1
                    ? $"<strong>{itemKvp[0]}</strong>: {itemKvp[1]}<br />"
                    : $"<p>{item}</p>" );
            }
        }
    }
}
