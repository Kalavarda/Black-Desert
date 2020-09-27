using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Barter.Model;

namespace Barter.UserControls
{
    public partial class TradeItemControl
    {
        private TradeItem _tradeItem;

        public TradeItem TradeItem
        {
            get => _tradeItem;
            set
            {
                if (value == _tradeItem)
                    return;

                _tradeItem = value;

                if (value != null)
                    ToolTip = value.ToString();
                else
                    ToolTip = null;

                DataContext = value;

                ItemChanged?.Invoke(this);
            }
        }

        public event Action<TradeItemControl> ItemChanged;

        public Func<string, IReadOnlyCollection<TradeItem>> Search;

        public TradeItemControl()
        {
            InitializeComponent();
        }

        private void OnSelectClick(object sender, RoutedEventArgs e)
        {
            if (Search == null)
                return;

            var window = new SearchItemWindow(Search) { Owner = Window.GetWindow(this) };
            if (window.ShowDialog() == true)
                TradeItem = window.SelectedTradeItem;
        }
    }

    public class TradeItemConverter: IValueConverter
    {
        private static readonly IDictionary<string, ImageSource> _images = new Dictionary<string, ImageSource>();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            if (value is TradeItem tradeItem)
            {
                if (string.IsNullOrWhiteSpace(tradeItem.ImageAddress))
                    return null;
                if (!_images.ContainsKey(tradeItem.ImageAddress))
                    _images.Add(tradeItem.ImageAddress, new BitmapImage(new Uri(tradeItem.ImageAddress)));
                return _images[tradeItem.ImageAddress];
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
