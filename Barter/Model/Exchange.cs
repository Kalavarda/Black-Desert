using System;

namespace Barter.Model
{
    public class Exchange
    {
        private TradeItem _sourceItem;
        private TradeItem _destItem;
        private int _ratio = 1;
        private int _count = 1;
        private int _sourceItemCount = 1;

        public TradeItem SourceItem
        {
            get => _sourceItem;
            set
            {
                if (_sourceItem == value)
                    return;
                _sourceItem = value;
                Changed?.Invoke(this);
            }
        }

        public TradeItem DestItem
        {
            get => _destItem;
            set
            {
                if (_destItem == value)
                    return;
                _destItem = value;
                Changed?.Invoke(this);
            }
        }

        public int Ratio
        {
            get => _ratio;
            set
            {
                if (_ratio == value)
                    return;
                _ratio = value;
                Changed?.Invoke(this);
            }
        }

        public int Count
        {
            get => _count;
            set
            {
                if (_count == value)
                    return;
                _count = value;
                Changed?.Invoke(this);
            }
        }

        public int SourceItemCount
        {
            get => _sourceItemCount;
            set
            {
                if (_sourceItemCount == value)
                    return;
                _sourceItemCount = value;
                Changed?.Invoke(this);
            }
        }

        public event Action<Exchange> Changed;
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
