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
        private readonly ITranslateService _translateService;
        private static readonly char[] Space = { ' ' };

        public SearchEngine(ITradeItemsDatabase tradeItemsDatabase, ITranslateService translateService)
        {
            _tradeItemsDatabase = tradeItemsDatabase ?? throw new ArgumentNullException(nameof(tradeItemsDatabase));
            _translateService = translateService ?? throw new ArgumentNullException(nameof(translateService));
        }

        public IReadOnlyCollection<TradeItem> Search(string value, TradeItemLevel? level = null)
        {
            if (string.IsNullOrWhiteSpace(value))
                return EmptySeaarchResult;

            var items = _tradeItemsDatabase.GetAllItems(level);

            var result = new List<TradeItem>();

            Search(value, items, result);
            if (result.Count == 0)
                Search(_translateService.ToAnotherKeyboardLayout(value), items, result);

            return result;
        }

        private static void Search(string value, IEnumerable<TradeItem> items, ICollection<TradeItem> result)
        {
            var words = value.ToLowerInvariant().Split(Space, StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in items)
            {
                var itemWords = item.Name.ToLowerInvariant().Split(Space, StringSplitOptions.RemoveEmptyEntries);
                if (words.All(word => itemWords.Any(w => w.Contains(word))))
                    result.Add(item);
            }
        }
    }
}
