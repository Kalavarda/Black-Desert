using System;
using System.Collections.Generic;
using System.Linq;

namespace Barter.Model
{
    public class Voyage
    {
        public Ship Ship { get; set; }

        public IReadOnlyCollection<Exchange> Exchanges { get; private set; } = new Exchange[0];

        public event Action<Voyage, Exchange> ExchangeAdded;

        public void Add(Exchange exchange)
        {
            if (exchange == null) throw new ArgumentNullException(nameof(exchange));

            Exchanges = new List<Exchange>(Exchanges) { exchange }.ToArray();
            ExchangeAdded?.Invoke(this, exchange);
        }
    }

    public static class VoyageExtensions
    {
        public static int GetSourceMass(this Voyage voyage, ITradeItemsDatabase itemsDatabase)
        {
            if (voyage == null) throw new ArgumentNullException(nameof(voyage));
            if (itemsDatabase == null) throw new ArgumentNullException(nameof(itemsDatabase));

            return voyage.Exchanges
                .Sum(ex => ex.GetSourceMass(itemsDatabase).GetValueOrDefault());
        }

        public static int GetDestMass(this Voyage voyage, ITradeItemsDatabase itemsDatabase)
        {
            if (voyage == null) throw new ArgumentNullException(nameof(voyage));
            if (itemsDatabase == null) throw new ArgumentNullException(nameof(itemsDatabase));

            if (voyage.Exchanges.Count == 0)
                return 0;

            return voyage.Exchanges
                .Sum(ex => ex.GetDestMass(itemsDatabase).GetValueOrDefault());
        }

        public static string GetWarning(this Voyage voyage, ITradeItemsDatabase itemsDatabase)
        {
            if (voyage == null) throw new ArgumentNullException(nameof(voyage));
            if (itemsDatabase == null) throw new ArgumentNullException(nameof(itemsDatabase));

            if (voyage.GetSourceMass(itemsDatabase) > voyage.Ship.MaxMass || voyage.GetDestMass(itemsDatabase) > voyage.Ship.MaxMass)
                return "Перегруз";

            return string.Empty;
        }

        public static string GetError(this Voyage voyage, ITradeItemsDatabase itemsDatabase)
        {
            if (voyage == null) throw new ArgumentNullException(nameof(voyage));
            if (itemsDatabase == null) throw new ArgumentNullException(nameof(itemsDatabase));

            var sourceMass = 0;
            var destMass = 0;
            var orevMass = false;
            foreach (var exchange in voyage.Exchanges)
                for (var i = 0; i < exchange.Count; i++)
                {
                    sourceMass += itemsDatabase.GetMass(exchange.SourceItem.Level);
                    destMass += itemsDatabase.GetMass(exchange.DestItem.Level) * exchange.Ratio;
                    if (sourceMass > voyage.Ship.MaxMass || destMass > voyage.Ship.MaxMass)
                    {
                        if (!orevMass)
                            orevMass = true;
                        else
                            return "Не влезет";
                    }
                }

            return string.Empty;
        }
    }
}
