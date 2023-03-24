using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeApp
{
    internal class NullTradeDataStore : ITradeDataStore
    {
        public void Save(IEnumerable<TradeRecord> trades)
        {
        }
    }
}
