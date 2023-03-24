using System.Data;
using System.Data.SqlClient;

namespace TradeApp
{
    internal class TradeProcessor
    {
        public TradeProcessor( ILogger logger, 
            ITradeDataProvider provider, ITradeDataParser parser, ITradeDataStore store ) 
        {
            Logger = logger;
            Provider = provider;
            Parser = parser;
            Store = store;
        }

        private ILogger Logger { get; }
        
        private ITradeDataProvider Provider { get; set; }

        private ITradeDataParser Parser { get; }

        private ITradeDataStore Store { get; set; }


        public void ProcessTrades(Stream stream)
        {
            var lines = Provider.GetTradeData();
            var trades = Parser.Parse(lines);
            Store.Save(trades );

            Logger?.Log( "INFO: {0} trades processed", trades.Count() );
        }
    }
}