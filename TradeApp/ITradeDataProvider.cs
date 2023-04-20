namespace TradeApp
{
    public interface ITradeDataProvider
    {
        IEnumerable<string> GetData();
    }
}
