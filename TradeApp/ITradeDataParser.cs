using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeApp
{
    internal interface ITradeDataParser
    {
        IEnumerable<TradeRecord> Parse( IEnumerable<string> lines );
    }
}
