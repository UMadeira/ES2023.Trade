using System.IO;

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

                var processor = new TradeProcessor();
                processor.ProcessTrades( stream );
                
            }
            catch ( Exception ex) 
            { 
                Console.WriteLine( ex.Message );
            }
        }
    }
}