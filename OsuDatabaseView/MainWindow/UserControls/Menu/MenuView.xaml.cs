
using System.Windows.Controls;
using System.Windows.Data;
using CommunityToolkit.Mvvm.Input;
using OsuDatabaseControl.Enums.Display;
using OsuDatabaseView.Utils.Converters;
using Binding = System.Windows.Data.Binding;

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
            CreateColumnVisibilityMenuItems();
        }
        
        public void SetMainWindowViewModel(MainWindowViewModel mainWindowViewModel)
        {
            if (DataContext is MenuViewModel menuViewModel)
            {
                menuViewModel.MainWindowViewModel = mainWindowViewModel;
            }
        }

        /// <summary>
        /// Creates the menu items for the view->column_visibility item menu based on the MainColumnVisibility enum
        /// </summary>
        private void CreateColumnVisibilityMenuItems()
        {
            foreach (MainColumnVisibility value in Enum.GetValues(typeof(MainColumnVisibility)))
            {
                if (value is MainColumnVisibility.All or MainColumnVisibility.Default) continue;

                if (value is MainColumnVisibility.BPM)
                {
                    ColumnVisibilityMenuItem.Items.Add(new Separator());    
                }
                
                var menuItem = new MenuItem
                {
                    StaysOpenOnClick = true, 
                    Header = value.ToString(),
                    Command = new RelayCommand(() =>
                    {
                        if (DataContext is MenuViewModel menuViewModel)
                        {
                            menuViewModel.ChangeMainColumnVisibilityStateCommand.Execute(value.ToString());
                        }
                    })
                };
                Binding binding = new Binding("MainColumnVisibilityState")
                {
                    Source = DataContext,
                    Converter = new MainColumnBoolConverter(),
                    ConverterParameter = value.ToString()
                };
                menuItem.SetBinding(MenuItem.IsCheckedProperty, binding);
                ColumnVisibilityMenuItem.Items.Add(menuItem);
            }
        }
    }
}
