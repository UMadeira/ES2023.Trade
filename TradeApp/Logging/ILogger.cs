namespace TradeApp.Logging
{
    public interface ILogger
    {
        void Log(string message, params object[] parameters);
    }
}
