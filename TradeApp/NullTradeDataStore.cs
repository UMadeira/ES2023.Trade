namespace TradeApp
{
    public class NullTradeDataStore : ITradeDataStore
    {
        public void Save(IEnumerable<TradeRecord> records)
        {
        }
    }
}
