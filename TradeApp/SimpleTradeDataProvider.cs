﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeApp
{
    public class SimpleTradeDataProvider : ITradeDataProvider
    {
        public SimpleTradeDataProvider( Stream stream ) 
        { 
            Stream = stream;
        }

        private Stream Stream { get; }

        public IEnumerable<string> GetData()
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
