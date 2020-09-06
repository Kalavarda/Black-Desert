using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Barter.Model;

[assembly: InternalsVisibleTo("Barter.Tests")]

namespace Barter.Impl
{
    public class SearchEngine: ISearchEngine
    {
        private static readonly TradeItem[] EmptySeaarchResult = new TradeItem[0];

        private readonly ITradeItemsDatabase _tradeItemsDatabase;
        private static readonly char[] Space = new []{' '};

        public SearchEngine(ITradeItemsDatabase tradeItemsDatabase)
        {
            _tradeItemsDatabase = tradeItemsDatabase ?? throw new ArgumentNullException(nameof(tradeItemsDatabase));
        }

        public IReadOnlyCollection<TradeItem> Search(string value, TradeItemLevel? level = null)
        {
            if (string.IsNullOrWhiteSpace(value))
                return EmptySeaarchResult;

            var items = _tradeItemsDatabase.GetAllItems(level);

            var result = new List<TradeItem>();

            var words = value.ToLowerInvariant().Split(Space, StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in items)
            {
                var itemWords = item.Name.ToLowerInvariant().Split(Space, StringSplitOptions.RemoveEmptyEntries);
                if (words.All(word => itemWords.Any(w => w.Contains(word))))
                    result.Add(item);
            }

            return result;
        }
    }
}
