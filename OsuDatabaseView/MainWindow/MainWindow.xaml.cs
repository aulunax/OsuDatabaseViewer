using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CommunityToolkit.Mvvm.Input;
using OsuDatabaseControl.DataTypes;
using OsuDatabaseView.MainWindow.UserControls.Menu;
using OsuDatabaseView.MainWindow.UserControls.ScoreInfo;

namespace OsuDatabaseView.MainWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ScoreInfoViewModel _scoreInfoViewModel;
        public MainWindow()
        {
            InitializeComponent();
            
            var mainWindowViewModel = new MainWindowViewModel();
            DataContext = mainWindowViewModel;
            
            menuView.SetMainWindowViewModel(mainWindowViewModel);
            
            _scoreInfoViewModel = new ScoreInfoViewModel();
            scoreInfoView.DataContext = _scoreInfoViewModel;
            mainWindowViewModel.SelectionChangedCommand = new RelayCommand<FullScore>(UpdateScoreInfoViewModel);

        }
        
        private void UpdateScoreInfoViewModel(FullScore selectedScoreInfo)
        {
            _scoreInfoViewModel.ScoreInfo = selectedScoreInfo;
        }

    }
}