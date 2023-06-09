﻿namespace TradeApp
{
    public class SimpleTradeDataParser : ITradeDataParser
    {
        public SimpleTradeDataParser( 
            ITradeDataValidator validator, ITradeDataMapper mapper ) 
        { 
            Validator = validator;
            Mapper = mapper;
        }

        private ITradeDataValidator Validator { get; }

        private ITradeDataMapper Mapper { get; }

        public IEnumerable<TradeRecord> Parse(IEnumerable<string> lines)
        {
            var trades = new List<TradeRecord>();

            foreach (var line in lines)
            {
                var fields = line.Split(new char[] { ',' });

                if ( ! Validator.Validate(fields ) ) continue;

                var record = Mapper.Map(fields);
                trades.Add(record);
            }

            return trades;
        }
    }
}
