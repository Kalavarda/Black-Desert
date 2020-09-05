using System.Windows;
using Barter.Model;

namespace Barter
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var ship = new Ship { Name = "Фрегат", MaxLT = 9200 };
        }
    }
}
