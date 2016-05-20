using System;
using Microsoft.Extensions.Logging;

namespace Loggr.Extensions.Logging.Console
{
    public class Program
    {
        public static void Main( string[] args )
        {
            var factory = new LoggerFactory();
            var logger = factory.CreateLogger( "MyLog" );
            factory.AddLoggr( "logKey", "apikey" );

            logger.LogCritical( "This is critical", new Exception( "Critical stuff going down" ) );
            logger.LogError( "This is an error", new Exception( "Erroneous stuff all over the place" ) );
            logger.LogInformation( "This is information" );
            logger.LogTrace( "This is trace" );
            logger.LogWarning( "This is a warning" );

            using( logger.BeginScope( "Main" ) )
            {
                logger.LogInformation( "This is information in scope" );
            }

            System.Console.ReadLine();
        }
    }
}
