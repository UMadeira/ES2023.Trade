using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeApp
{
    internal class ConsoleLogger : ILogger
    {
        public void Log(string message, params object[] parameters)
        {
            Console.WriteLine( message, parameters );
        }
    }
}
