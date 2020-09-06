using System;

namespace Barter.Model
{
    public class Exchange
    {
        public TradeItem SourceItem { get; set; }

        public TradeItem DestItem { get; set; }
        
        public int Ratio { get; set; } = 1;

        public int Count { get; set; } = 1;

        public int SourceItemCount { get; set; } = 1;
    }

    public static class ExchangeExtensions
    {
        public static int? GetSourceMass(this Exchange exchange, ITradeItemsDatabase itemsDatabase)
        {
            if (exchange == null) throw new ArgumentNullException(nameof(exchange));
            if (itemsDatabase == null) throw new ArgumentNullException(nameof(itemsDatabase));

            if (exchange.SourceItem == null)
                return null;

            return itemsDatabase.GetMass(exchange.SourceItem.Level) * exchange.SourceItemCount * exchange.Count;
        }

        public static int? GetDestMass(this Exchange exchange, ITradeItemsDatabase itemsDatabase)
        {
            if (exchange == null) throw new ArgumentNullException(nameof(exchange));
            if (itemsDatabase == null) throw new ArgumentNullException(nameof(itemsDatabase));

            if (exchange.DestItem == null)
                return null;

            return itemsDatabase.GetMass(exchange.DestItem.Level) * exchange.Ratio * exchange.Count;
        }
    }
}
