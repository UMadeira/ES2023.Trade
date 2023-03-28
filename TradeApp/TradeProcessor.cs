using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace TradeApp
{
    public class TradeProcessor
    {
        public TradeProcessor( 
            ITradeDataProvider provider, ITradeDataParser parser, ITradeDataStore store ) 
        { 
            Provider  = provider;
            Parser    = parser; 
            DataStore = store;
        }

        private ITradeDataProvider Provider { get; }
        private ITradeDataParser Parser { get; }
        private ITradeDataStore    DataStore { get; }


        public void ProcessTrades()
        {
            var lines = Provider.GetData();
            var trades = Parser.Parse(lines);
            DataStore.Save( trades );

            Log("INFO: {0} trades processed", trades.Count() );
        }

        private void Log( string message, params object[] parameters )
        {
            Console.WriteLine( message, parameters );
        }

    }
}