using Barter.Model;

namespace Barter
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _voyageControl.Voyage = new Voyage
            {
                Ship = App.Ship
            };
        }
    }
}
