using System.Collections.Generic;
using Barter.Model;

namespace Barter
{
    public interface ITradeItemsDatabase
    {
        IReadOnlyCollection<TradeItem> GetAllItems();

        int GetMass(TradeItemLevel level);

        int GetCost(TradeItemLevel level);
    }
}
