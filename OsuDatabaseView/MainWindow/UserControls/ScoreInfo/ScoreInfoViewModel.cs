using System.ComponentModel;
using OsuDatabaseControl.Config;
using OsuDatabaseControl.DataTypes;

namespace OsuDatabaseView.MainWindow.UserControls.ScoreInfo;

public class ScoreInfoViewModel : INotifyPropertyChanged
{
    private FullScore? _scoreInfo;

    public FullScore? ScoreInfo
    {
        get => _scoreInfo;
        set
        {
            _scoreInfo = value;
            OnPropertyChanged(nameof(ScoreInfo));


        }
    }
    

    
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}