using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OsuDatabaseView.MainWindow.UserControls.Menu
{
    /// <summary>
    /// Interaction logic for MenuView.xaml
    /// </summary>
    public partial class MenuView : System.Windows.Controls.UserControl
    {
        public MenuView()
        {
            InitializeComponent();
            DataContext = new MenuViewModel();
        }
        
        public void SetMainWindowViewModel(MainWindowViewModel mainWindowViewModel)
        {
            if (DataContext is MenuViewModel menuViewModel)
            {
                menuViewModel.MainWindowViewModel = mainWindowViewModel;
            }
        }
    }
}
