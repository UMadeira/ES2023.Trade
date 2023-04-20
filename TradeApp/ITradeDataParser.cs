namespace TradeApp
{
    public interface ITradeDataParser
    {
        IEnumerable<TradeRecord> Parse( IEnumerable<string> lines );
    }
}
