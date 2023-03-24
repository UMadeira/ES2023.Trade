using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeApp
{
    internal class CompositeLogger : ILogger
    {
        public CompositeLogger( params ILogger[] loggers ) 
        { 
            foreach ( var logger in loggers )
                Loggers.Add( logger );
        }

        private IList<ILogger> Loggers { get; } = new List<ILogger>();

        public void Add( ILogger logger ) 
        {
            Loggers.Add(logger);
        }

        public void Log(string message, params object[] parameters)
        {
            foreach ( var logger in Loggers )
                logger.Log(message, parameters );
        }
    }
}
