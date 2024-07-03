using UserControl = System.Windows.Controls.UserControl;

namespace OsuDatabaseView.MainWindow.UserControls.ScoreInfo;

public partial class ScoreInfoView : UserControl
{
    public ScoreInfoView()
    {
        InitializeComponent();
        DataContext = new ScoreInfoViewModel();
    }
    
}