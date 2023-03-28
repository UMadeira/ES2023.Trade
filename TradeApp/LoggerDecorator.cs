using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeApp
{
    internal abstract class LoggerDecorator : ILoggerDecorator
    {
        public LoggerDecorator(ILogger component)
        {
            Component = component;
        }

        public ILogger Component { get; }

        public virtual void Log(string message, params object[] parameters)
        {
            Component.Log(message, parameters);
        }
    }
}
