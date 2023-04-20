﻿using TradeApp.Logging;

namespace TradeApp
{
    public class SimpleTradeDataMapper : ITradeDataMapper
    {
        public SimpleTradeDataMapper( ILogger logger ) 
        { 
            Logger = logger;
        }

        private ILogger Logger { get; set; }

        public TradeRecord Map(string[] fields)
        {
            Logger.Log($"Map: { string.Join(',', fields) }");

            var sourceCurrencyCode = fields[0].Substring(0, 3);
            var destinationCurrencyCode = fields[0].Substring(3, 3);
            var tradeAmount = int.Parse(fields[1]);
            var tradePrice = decimal.Parse(fields[2]);

            var trade = new TradeRecord
            {
                SourceCurrency = sourceCurrencyCode,
                DestinationCurrency = destinationCurrencyCode,
                Lots = tradeAmount / LotSize,
                Price = tradePrice
            };

            return trade;
        }

        private static float LotSize = 100000f;
    }
}
