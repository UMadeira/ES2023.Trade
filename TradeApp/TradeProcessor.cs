using TradeApp.Logging;

namespace TradeApp
{
    public class TradeProcessor
    {
        public TradeProcessor( ILogger logger,
            ITradeDataProvider provider, ITradeDataParser parser, ITradeDataStore store ) 
        { 
            Logger    = logger;
            Provider  = provider;
            Parser    = parser; 
            DataStore = store;
        }

        private ILogger Logger { get; }

        private ITradeDataProvider Provider { get; }
        private ITradeDataParser Parser { get; }
        private ITradeDataStore    DataStore { get; }


        public void ProcessTrades()
        {
            var lines = Provider.GetData();
            var trades = Parser.Parse(lines);
            DataStore.Save( trades );

            Logger.Log("INFO: {0} trades processed", trades.Count() );
        }

    }
}