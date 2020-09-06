using System.Collections.Generic;
using Barter.Model;

namespace Barter
{
    public interface ISearchEngine
    {
        IReadOnlyCollection<TradeItem> Search(string value, TradeItemLevel? level = null);
    }
}
