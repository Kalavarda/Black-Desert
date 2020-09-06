using System.Diagnostics;
using System.IO;
using System.Linq;
using Barter.Impl;
using Barter.Model;
using Microsoft.Win32;

namespace Barter
{
    public partial class MainWindow
    {
        private ITradeItemsDatabase _itemsDatabase;
        private ISearchEngine _searchEngine;

        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            while (string.IsNullOrWhiteSpace(Settings.Default.ItemsFileName) || !File.Exists(Settings.Default.ItemsFileName))
            {
                var fileDialog = new OpenFileDialog
                {
                    Title = "Укажите файл с данными",
                    DefaultExt = ".json",
                    Filter = "json|*.json|all files|*.*"
                };
                if (fileDialog.ShowDialog() == true)
                    Settings.Default.ItemsFileName = fileDialog.FileName;
            }
            Settings.Default.Save();

            _itemsDatabase = new TradeItemsDatabase(Settings.Default.ItemsFileName);
            _searchEngine = new SearchEngine(_itemsDatabase);

            Temp();

            _tradeItemControl.Search = s => _searchEngine.Search(s);
            _tradeItemControl.TradeItem = _searchEngine.Search("Синий кварц").Single();
        }

        private void Temp()
        {
            var ship = new Ship {Name = "Фрегат", MaxMass = 9200};

            var exchange1 = new Exchange
            {
                SourceItem = _searchEngine.Search("Осколок аметиста").Single(),
                Ratio = 1,
                DestItem = _searchEngine.Search("Восьмигранный сундук").Single(),
                Count = 4
            };
            var exchange2 = new Exchange
            {
                SourceItem = _searchEngine.Search("бусы").Single(),
                Ratio = 4,
                DestItem = _searchEngine.Search("часы").Single(),
                Count = 2
            };

            var voyage = new Voyage
            {
                Ship = ship,
                Exchanges = new[] {exchange1, exchange2}
            };

            var sourceMass = voyage.GetSourceMass(_itemsDatabase);
            Debug.WriteLine(sourceMass);

            var destMass = voyage.GetDestMass(_itemsDatabase);
            Debug.WriteLine(destMass);

            var warning = voyage.GetWarning(_itemsDatabase);
            Debug.WriteLine(warning);

            var error = voyage.GetError(_itemsDatabase);
            Debug.WriteLine(error);
        }
    }
}
