using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;
using TradeApp.Logging;

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

            var filename = config["LOG_FILE"];
            var token = config["TOKEN"];
                
            var services = new ServiceCollection();

            ILogger logger = new CompositeLogger(
                new NumberDecoratorLogger(new ConsoleLogger()),
                new DateDecoratorLogger(new FileLogger("log.txt")));

            services.AddSingleton<ILogger>( 
                (sp) => new DateDecoratorLogger(new FileLogger(filename)));

            services.AddSingleton<ITradeDataProvider>(
                (sp) => new SimpleTradeDataProvider(File.OpenRead("TradeData.txt" ) ) );
            services.AddScoped<ITradeDataParser, SimpleTradeDataParser>();
            services.AddScoped<ITradeDataValidator, SimpleTradeDataValidator>();
            services.AddScoped<ITradeDataMapper, SimpleTradeDataMapper>();
            services.AddScoped<ITradeDataStore, NullTradeDataStore>();
            services.AddTransient<TradeProcessor>();

            var serviceProvider = services.BuildServiceProvider();

            var processor = serviceProvider.GetService<TradeProcessor>();
            processor.ProcessTrades();
        }
    }
}