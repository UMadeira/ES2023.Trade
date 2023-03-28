using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeApp
{
    public class SimpleTradeDataValidator : ITradeDataValidator
    {
        public bool Validate( string[] fields )
        {
            if (fields.Length != 3)
            {
                Log("WARN: Data {0} malformed. Only {1} field(s) found.",
                    string.Join(',', fields ), fields.Length);
                return false;
            }
            if (fields[0].Length != 6)
            {
                Log("WARN: Trade currencies on data {0} malformed: '{1}'",
                    string.Join(',', fields), fields[0]);
                return false;
            }
            if (!int.TryParse(fields[1], out var tradeAmount))
            {
                Log("WARN: Trade amount on data {0} not a valid integer: '{1}'",
                    string.Join(',', fields), fields[1]);
                return false;
            }
            if (!decimal.TryParse(fields[2], out var tradePrice))
            {
                Log("WARN: Trade price on data {0} not a valid decimal: '{1}'",
                    string.Join(',', fields), fields[2]);
                return false;
            }
            return true;
        }

        private void Log(string message, params object[] parameters)
        {
            Console.WriteLine(message, parameters);
        }
    }
}
