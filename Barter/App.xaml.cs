using System.IO;
using Barter.Impl;
using Barter.Model;
using Microsoft.Win32;

namespace Barter
{
    public partial class App
    {
        public static ITradeItemsDatabase TradeItemsDatabase { get; }
        
        public static ISearchEngine SearchEngine { get; }

        public static Ship Ship { get; }

        static App()
        {
            Ship = new Ship { Name = "Фрегат", MaxMass = 9200 };

            while (string.IsNullOrWhiteSpace(Settings.Default.ItemsFileName) || !File.Exists(Settings.Default.ItemsFileName))
            {
                var fileDialog = new OpenFileDialog
                {
                    Title = "Укажите файл с данными",
                    DefaultExt = ".json",
                    Filter = "json|*.json|all files|*.*"
                };
                if (fileDialog.ShowDialog() == true)
                {
                    Settings.Default.ItemsFileName = fileDialog.FileName;
                    Settings.Default.Save();
                }
            }

            TradeItemsDatabase = new TradeItemsDatabase(Settings.Default.ItemsFileName);

            SearchEngine = new SearchEngine(TradeItemsDatabase, new TranslateService());
        }
    }
}
