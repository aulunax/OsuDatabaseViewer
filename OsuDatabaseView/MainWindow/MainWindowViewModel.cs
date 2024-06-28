using Microsoft.VisualBasic;
using OsuDatabaseControl.IO.Readers;
using OsuDatabaseControl.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OsuDatabaseControl.Config;
using OsuDatabaseControl.DataAccess;
using OsuDatabaseControl.DataTypes.Osu;
using OsuDatabaseControl.DTO;


namespace OsuDatabaseView.MainWindow
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ScoreAndBeatmapPrintable> _scores;

        public ObservableCollection<ScoreAndBeatmapPrintable> Scores
        {
            get { return _scores; }
            set
            {
                _scores = value;
                OnPropertyChanged(nameof(Scores));
            }
        }

        public MainWindowViewModel()
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                string osuPath = ConfigManager.Instance.Config.OsuDirectory;
                if (!Directory.Exists(osuPath))
                {
                    return;
                }
                Scores scores = ScoresDBReader.ReadScores(Path.Combine(osuPath, FilePaths.OSU_SCOREDB_FILENAME));
                Beatmaps beatmaps = OsuDBReader.ReadBeatmaps(Path.Combine(osuPath, FilePaths.OSU_OSUDB_FILENAME));
                Scores = new ObservableCollection<ScoreAndBeatmapPrintable>();
                BeatmapDictionary beatmapDictionary = new BeatmapDictionary(beatmaps);
                foreach (Score sc in scores.GetScores())
                {
                    try
                    {
                        Scores.Add(new ScoreAndBeatmapPrintable(sc, beatmapDictionary));
                    }
                    catch (KeyNotFoundException ex) {
                        Debug.WriteLine($"Beatmap Hash is not present in the dictionary: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Exception occurred in when adding a Score in MainWindowViewModel: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception occurred in LoadData: {ex.Message}");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
