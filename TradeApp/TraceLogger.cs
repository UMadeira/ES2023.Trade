using System.Diagnostics;
using TradeApp.Logging;

namespace TradeApp
{
    public class TraceLogger : ILogger
    {
        public void Log(string message, params object[] parameters)
        {
            Trace.WriteLine(
                string.Format( message, parameters ) );
        }
    }
}
