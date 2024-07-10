using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using OsuDatabaseControl.Config;
using OsuDatabaseControl.DataTypes;
using OsuFormatReader.Enums;
using OsuFormatReader.IO;
using OsuFormatReader.Sections;
using OsuFormatReader.Sections.EventTypes;

namespace OsuDatabaseView.MainWindow.UserControls.ScoreInfo;

public class ScoreInfoViewModel : INotifyPropertyChanged
{
    private string _imagePath;

    public string ImagePath
    {
        get => _imagePath;
        set
        {
            _imagePath = value;
            OnPropertyChanged(nameof(ImagePath));
        }
    }

    private FullScore _scoreInfo;

    public FullScore ScoreInfo
    {
        get => _scoreInfo;
        set
        {
            _scoreInfo = value;
            OnPropertyChanged(nameof(ScoreInfo));
            if (_scoreInfo is null)
            {
                ImagePath = null;
                return; 
            }
            try
            {
                using (OsuFormatStreamReader reader =
                       new OsuFormatStreamReader(
                           new FileStream(
                               Path.Combine(ConfigManager.Instance.Config.OsuDirectory, "Songs", _scoreInfo.FolderName,
                                   _scoreInfo.OsuFileName), FileMode.Open)))
                {
                    Events events = Events.Read(reader);
                    BackgroundsEvent bgEvent =
                        (BackgroundsEvent)events.GetEventsList()
                            .FirstOrDefault(e => e.eventType == EventType.Background);
                    if (bgEvent != null)
                        ImagePath = Path.Combine(ConfigManager.Instance.Config.OsuDirectory, "Songs",
                            _scoreInfo.FolderName,
                            bgEvent.eventParams.filename);
                    else
                        ImagePath = null;
                }
            }
            catch (Exception e) when (e is IOException || e is OutOfMemoryException || e is ArgumentException ||
                                      e is ArgumentNullException)
            {
                Debug.WriteLine(e.ToString());
                ImagePath = null;
            }
        }
    }


    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}