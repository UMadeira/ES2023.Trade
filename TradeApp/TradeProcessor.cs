using System.Data;
using System.Data.SqlClient;

namespace TradeApp
{
    public class TradeProcessor
    {
        public void ProcessTrades(Stream stream)
        {
            var lines = ReadTradeData(stream);
            var trades = ParseTradeData(lines);
            StoreTrades(trades );

            Console.WriteLine( "INFO: {0} trades processed", trades.Count() );
        }

        private IList<string> ReadTradeData(Stream stream)
        {
            var lines = new List<string>();
            using (var reader = new StreamReader(stream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
            return lines;
        }

        private IEnumerable<TradeRecord> ParseTradeData( IEnumerable<string> lines )
        {
            var trades = new List<TradeRecord>();

            var lineCount = 0;
            foreach (var line in lines)
            {
                lineCount++;

                var fields = line.Split(new char[] { ',' });

                if (!ValidateTradeData(fields, lineCount)) continue;
                var record = MapTradeDataToRecord(fields);

                trades.Add(record);
            }

            return trades;
        }

        private bool ValidateTradeData( string[] fields, int currentLine )
        {
            if (fields.Length != 3)
            {
                Console.WriteLine("WARN: Line {0} malformed. Only {1} field(s) found.",
                currentLine, fields.Length);
                return false;
            }
            if (fields[0].Length != 6)
            {
                Console.WriteLine("WARN: Trade currencies on line {0} malformed: '{1}'",
                currentLine, fields[0]);
                return false;
            }

            if (!int.TryParse(fields[1], out var tradeAmount))
            {
                Console.WriteLine("WARN: Trade amount on line {0} not a valid integer: '{1}'", currentLine, fields[1]);
                return false;
            }

            if (!decimal.TryParse(fields[2], out var tradePrice))
            {
                Console.WriteLine("WARN: Trade price on line {0} not a valid decimal: '{1}'", currentLine, fields[2]);
                return false;
            }

            return true;
        }

        private TradeRecord MapTradeDataToRecord( string[] fields )
        {
            var sourceCurrencyCode = fields[0].Substring( 0, 3 );
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

        private void StoreTrades( IEnumerable<TradeRecord> records )
        {
            using (var connection = new SqlConnection("Data Source=(local); Initial Catalog=TradeDatabase; Integrated Security=True"))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    foreach (var trade in records )
                    {
                        var command = connection.CreateCommand();
                        command.Transaction = transaction;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "dbo.insert_trade";
                        command.Parameters.AddWithValue("@sourceCurrency", trade.SourceCurrency);
                        command.Parameters.AddWithValue("@destinationCurrency", trade.DestinationCurrency);
                        command.Parameters.AddWithValue("@lots", trade.Lots);
                        command.Parameters.AddWithValue("@price", trade.Price);
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                connection.Close();
            }

        }

        private static float LotSize = 100000f;
    }
}