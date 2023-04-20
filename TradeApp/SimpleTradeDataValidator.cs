using TradeApp.Logging;

namespace TradeApp
{
    public class SimpleTradeDataValidator : ITradeDataValidator
    {
        public SimpleTradeDataValidator( ILogger logger ) 
        { 
            Logger = logger;
        }

        private ILogger Logger { get; }

        public bool Validate( string[] fields )
        {
            if (fields.Length != 3)
            {
                Logger.Log("WARN: Data '{0}' malformed. Only {1} field(s) found.",
                    string.Join(',', fields ), fields.Length);
                return false;
            }
            if (fields[0].Length != 6)
            {
                Logger.Log("WARN: Trade currencies on data '{0}' malformed: '{1}'",
                    string.Join(',', fields), fields[0]);
                return false;
            }
            if (!int.TryParse(fields[1], out var tradeAmount))
            {
                Logger.Log("WARN: Trade amount on data '{0}' not a valid integer: '{1}'",
                    string.Join(',', fields), fields[1]);
                return false;
            }
            if (!decimal.TryParse(fields[2], out var tradePrice))
            {
                Logger.Log("WARN: Trade price on data '{0}' not a valid decimal: '{1}'",
                    string.Join(',', fields), fields[2]);
                return false;
            }
            return true;
        }
    }
}
