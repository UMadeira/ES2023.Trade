using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeApp
{
    internal class TraceLogger : ILogger
    {
        public TraceLogger() 
        { 
        }
        public void Log(string message, params object[] parameters)
        {
            System.Diagnostics.Trace.WriteLine(
                string.Format( message, parameters ) );
        }
    }
}
