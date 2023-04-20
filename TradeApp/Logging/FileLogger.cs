namespace TradeApp.Logging
{
    public class FileLogger : ILogger
    {
        public FileLogger(string filename)
        {
            Writer = new StreamWriter(filename) { AutoFlush = true };
        }
        private StreamWriter Writer { get; }

        public void Log(string message, params object[] parameters)
        {
            Writer.WriteLine(message, parameters);
        }
    }
}
