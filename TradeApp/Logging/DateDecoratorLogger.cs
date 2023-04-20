using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeApp.Logging
{
    public class DateDecoratorLogger : DecoratorLogger
    {
        public DateDecoratorLogger(ILogger component)
            : base(component)
        {
        }

        public override void Log(string message, params object[] parameters)
        {
            var time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            message = $"{time} - {message}";
            base.Log(message, parameters);
        }
    }
}
