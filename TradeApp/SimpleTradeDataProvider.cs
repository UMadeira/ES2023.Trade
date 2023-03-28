using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeApp
{
    internal class SimpleTradeDataProvider : ITradeDataProvider
    {
        public SimpleTradeDataProvider( Stream stream ) 
        { 
            Stream = stream;
        }

        private Stream Stream { get; }

        public IEnumerable<string> GetTradeData()
        {
            var lines = new List<string>();
            using (var reader = new StreamReader( Stream ) )
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
            return lines;
        }
    }
}
