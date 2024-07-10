using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Image = System.Drawing.Image;
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