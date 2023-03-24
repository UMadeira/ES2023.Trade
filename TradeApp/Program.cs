using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TradeApp
{
    internal class Program
    {
        static void Main(string[] args)
        { 
      
            Console.WriteLine("TradeApp v1.0!");

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<Program>()
                .Build();

            var password = config["Password"];

            try
            {
                var filename = config["TradeFile"];
                using var stream = File.OpenRead(filename);

                var services = new ServiceCollection();
                services.AddSingleton<ILogger>( 
                    (sp) => 
                        new DateLoggerDecorator( 
                            new NumberLoggerDecorator(
                                new CompositeLogger( new ConsoleLogger(), new TraceLogger() ) ) ) );

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