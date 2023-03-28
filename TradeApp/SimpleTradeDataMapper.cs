using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeApp
{
    internal class SimpleTradeDataMapper : ITradeDataMapper
    {
        private static float LotSize = 100000f;

        public TradeRecord Map(string[] fields)
        {
            var sourceCurrencyCode = fields[0].Substring(0, 3);
            var destinationCurrencyCode = fields[0].Substring(3, 3);

            var amount = int.Parse(fields[1]);
            var price = decimal.Parse(fields[2]);

            var record = new TradeRecord()
            {
                SourceCurrency = sourceCurrencyCode,
                DestinationCurrency = destinationCurrencyCode,
                Lots = amount / LotSize,
                Price = price
            };

            return record;
        }
    }
}
