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
using System.Windows.Shapes;

namespace OsuDatabaseView.StartWindow.OsuDirectorySelection
{
    /// <summary>
    /// Interaction logic for OsuDirectorySelectionWindow.xaml
    /// </summary>
    public partial class OsuDirectorySelectionWindow : Window
    {
        public OsuDirectorySelectionWindow()
        {
            InitializeComponent();
            DataContext = new OsuDirectorySelectionWindowViewModel();
        }
        public object SelectedItem => ListBoxItems.SelectedItem;

        private void SelectItem_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
