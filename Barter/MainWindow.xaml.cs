using System.Diagnostics;
using System.Linq;
using Barter.Impl;
using Barter.Model;

namespace Barter
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            ITradeItemsDatabase itemsDatabase = new TradeItemsDatabase();
            ISearchEngine searchEngine = new SearchEngine(itemsDatabase);

            var ship = new Ship { Name = "Фрегат", MaxMass = 9200 };

            var exchange1 = new Exchange
            {
                SourceItem = searchEngine.Search("Осколок аметиста").Single(),
                Ratio = 1,
                DestItem = searchEngine.Search("Восьмигранный сундук").Single(),
                Count = 4
            };
            var exchange2 = new Exchange
            {
                SourceItem = searchEngine.Search("бусы").Single(),
                Ratio = 4,
                DestItem = searchEngine.Search("часы").Single(),
                Count = 2
            };

            var voyage = new Voyage
            {
                Ship = ship,
                Exchanges = new [] { exchange1, exchange2 }
            };

            var sourceMass = voyage.GetSourceMass(itemsDatabase);
            Debug.WriteLine(sourceMass);

            var destMass = voyage.GetDestMass(itemsDatabase);
            Debug.WriteLine(destMass);

            var warning = voyage.GetWarning(itemsDatabase);
            Debug.WriteLine(warning);

            var error = voyage.GetError(itemsDatabase);
            Debug.WriteLine(error);
        }
    }
}
