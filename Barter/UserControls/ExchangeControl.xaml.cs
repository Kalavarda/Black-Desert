using System.Windows.Controls;
using Barter.Model;

namespace Barter.UserControls
{
    public partial class ExchangeControl
    {
        private Exchange _exchange;

        public Exchange Exchange
        {
            get => _exchange;
            set
            {
                if (value == _exchange)
                    return;

                _exchange = value;

                if (value != null)
                {
                    _source.TradeItem = value.SourceItem;
                    _dest.TradeItem = value.DestItem;
                    _tbCount.Text = value.Count.ToString();
                    _tbSourceCount.Text = value.SourceItemCount.ToString();
                    _tbRatio.Text = value.Ratio.ToString();
                }
                else
                {
                    _source.TradeItem = null;
                    _dest.TradeItem = null;
                    _tbCount.Text = string.Empty;
                    _tbSourceCount.Text = string.Empty;
                    _tbRatio.Text = string.Empty;
                }

                TuneControls();
            }
        }

        public ISearchEngine SearchEngine { get; set; }

        public ITradeItemsDatabase TradeItemsDatabase { get; set; }

        public ExchangeControl()
        {
            InitializeComponent();

            _source.ItemChanged += control =>
            {
                if (Exchange != null)
                    Exchange.SourceItem = control.TradeItem;
                TuneControls();
            };
            _dest.ItemChanged += control =>
            {
                if (Exchange != null)
                    Exchange.DestItem = control.TradeItem;
                TuneControls();
            };

            _source.Search = s =>
            {
                if (SearchEngine == null)
                    return new TradeItem[0];
                return SearchEngine.Search(s);
            };
            _dest.Search = s =>
            {
                if (SearchEngine == null)
                    return new TradeItem[0];
                return SearchEngine.Search(s);
            };
        }

        private void TuneControls()
        {
            if (Exchange != null && TradeItemsDatabase != null)
            {
                var sourceMass = Exchange.GetSourceMass(TradeItemsDatabase);
                _tbSourceMass.Text = sourceMass != null
                    ? sourceMass.Value.ToString("### ### ###") + " LT"
                    : null;

                var destMass = Exchange.GetDestMass(TradeItemsDatabase);
                _tbDestMass.Text = destMass != null
                    ? destMass.Value.ToString("### ### ###") + " LT"
                    : null;
            }
            else
            {
                _tbSourceMass.Text = string.Empty;
            }
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (Exchange == null)
                return;

            try
            {
                Exchange.Count = int.Parse(_tbCount.Text);
                Exchange.SourceItemCount = int.Parse(_tbSourceCount.Text);
                Exchange.Ratio = int.Parse(_tbRatio.Text);
            }
            catch
            {
            }

            TuneControls();
        }
    }
}
