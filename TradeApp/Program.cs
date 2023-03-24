using System.IO;
using Microsoft.Extensions.DependencyInjection;

namespace TradeApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("TradeApp v1.0!");

            try
            {
                using var stream = File.OpenRead("TradeData.txt");

                var services = new ServiceCollection();
                services.AddSingleton<ILogger,ConsoleLogger>();
                services.AddSingleton<ITradeDataProvider>(
                    (sp) => new SimpleTradeDataProvider(stream));
                services.AddScoped<ITradeDataValidator, SimpleTradeDataValidator>();
                services.AddScoped<ITradeDataMapper, SimpleTradeDataMapper>();
                services.AddScoped<ITradeDataParser, SimpleTradeDataParser>();
                services.AddScoped<ITradeDataStore, NullTradeDataStore>();

                services.AddTransient<TradeProcessor>();
                
                var provider = services.BuildServiceProvider();

                var processor = provider.GetService<TradeProcessor>();
                processor?.ProcessTrades( stream );
                
            }
            catch ( Exception ex) 
            { 
                Console.WriteLine( ex.Message );
            }
        }
    }
}