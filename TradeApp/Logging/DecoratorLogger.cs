using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeApp.Logging
{
    public class DecoratorLogger : ILogger
    {
        public DecoratorLogger(ILogger component)
        {
            Component = component;
        }

        protected ILogger Component { get; private set; }

        public virtual void Log(string message, params object[] parameters)
        {
            Component.Log(message, parameters);
        }
    }
}
