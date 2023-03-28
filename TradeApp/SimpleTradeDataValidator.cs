using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeApp
{
    internal class SimpleTradeDataValidator : ITradeDataValidator
    {
        public SimpleTradeDataValidator( ILogger logger ) 
        {
            Logger = logger;
        }

        private ILogger Logger { get; }

        public bool Validate(string[] fields)
        {
            if (fields.Length != 3)
            {
                Logger?.Log("WARN: Only {0} field(s) found.", fields.Length );
                return false;
            }
            if (fields[0].Length != 6)
            {
                Logger?.Log("WARN: Trade currencies malformed: '{0}'", fields[0]);
                return false;
            }

            if (!int.TryParse(fields[1], out var tradeAmount))
            {
                Logger?.Log("WARN: Trade amount not a valid integer: '{0}'", fields[1]);
                return false;
            }

            if (!decimal.TryParse(fields[2], out var tradePrice))
            {
                Logger?.Log("WARN: Trade price not a valid decimal: '{0}'", fields[2]);
                return false;
            }

            return true;
        }
    }
}
