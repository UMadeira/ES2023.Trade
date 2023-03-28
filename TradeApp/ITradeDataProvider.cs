using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeApp
{
    public interface ITradeDataProvider
    {
        IEnumerable<string> GetData();
    }
}
