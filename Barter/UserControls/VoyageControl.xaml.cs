using System.Collections.ObjectModel;
using System.Windows;
using Barter.Model;

namespace Barter.UserControls
{
    public partial class VoyageControl
    {
        private Voyage _voyage;
        private readonly ObservableCollection<Exchange> _itemsSource = new ObservableCollection<Exchange>();

        public Voyage Voyage
        {
            get => _voyage;
            set
            {
                if (value == _voyage)
                    return;

                _voyage = value;

                if (value != null)
                    value.ExchangeAdded += Value_ExchangeAdded;
            }
        }

        private void Value_ExchangeAdded(Voyage voyage, Exchange exchange)
        {
            _itemsSource.Add(exchange);
        }

        public VoyageControl()
        {
            InitializeComponent();
            _itemsControl.ItemsSource = _itemsSource;
        }

        private void OnAddClick(object sender, RoutedEventArgs e)
        {
            var exchange = new Exchange();
            exchange.Changed += Exchange_Changed;
            Voyage?.Add(exchange);
        }

        private void Exchange_Changed(Exchange exchange)
        {
            var sourceMass = Voyage.GetSourceMass(App.TradeItemsDatabase);
            _tbSourceMass.Text = sourceMass.ToString("### ### ###") + " LT";
            
            var destMass = Voyage.GetDestMass(App.TradeItemsDatabase);
            _tbDestMass.Text = destMass.ToString("### ### ###") + " LT";
        }

        private void ExchangeControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            var exchangeControl = (ExchangeControl) sender;
        }
    }
}
