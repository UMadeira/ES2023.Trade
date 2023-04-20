namespace TradeApp
{
    public interface ITradeDataStore
    {
        void Save( IEnumerable<TradeRecord> records );
    }
}
