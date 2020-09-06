using System.Collections.Generic;
using Barter.Model;

namespace Barter
{
    public interface ITradeItemsDatabase
    {
        int GetMass(TradeItemLevel level);

        int GetCost(TradeItemLevel level);

        IReadOnlyCollection<TradeItem> GetAllItems(TradeItemLevel? level = null);
    }
}
