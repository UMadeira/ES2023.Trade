using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TradeApp
{
    internal class Program
    {
        static void Main(string[] args)
        { 
            Console.WriteLine("TradeApp v1.0!");

            using var stream = File.OpenRead( "TradeData.txt" );

            var provider = new SimpleTradeDataProvider( stream );
            var parser = new SimpleTradeDataParser(
                new SimpleTradeDataValidator(), new SimpleTradeDataMapper());
            var store = new NullTradeDataStore();

            var processor = new TradeProcessor( provider, parser, store );
            processor.ProcessTrades();
        }
    }
}