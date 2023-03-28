using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeApp
{
    internal class DateLoggerDecorator : LoggerDecorator
    {
        public DateLoggerDecorator(ILogger component) : base(component)
        {
        }

        public override void Log(string message, params object[] parameters)
        {
            var date = DateTime.Now.ToString( "yyyy-MM-dd HH:mm:ss");
            message = $"{date} - {message}";
            base.Log(message, parameters);
        }
    }
}
