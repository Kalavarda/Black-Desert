using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Barter.Model;

namespace Barter.UserControls
{
    public partial class SearchItemWindow
    {
        private readonly Func<string, IEnumerable<TradeItem>> _search;

        public TradeItem SelectedTradeItem => _cb.SelectedItem as TradeItem;

        public SearchItemWindow()
        {
            InitializeComponent();

            Loaded += (sender, e) =>
            {
                _cb.IsDropDownOpen = true;
                _cb.Focus();
            };
        }

        public SearchItemWindow(Func<string, IReadOnlyCollection<TradeItem>> search): this()
        {
            _search = search ?? throw new ArgumentNullException(nameof(search));
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedTradeItem != null)
                DialogResult = true;
        }

        private void ComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SelectedTradeItem != null && SelectedTradeItem.ToString() == _cb.Text)
                return;

            var items = _search(_cb.Text);
            _cb.ItemsSource = items.OrderBy(i => i.Name);
        }
    }
}
