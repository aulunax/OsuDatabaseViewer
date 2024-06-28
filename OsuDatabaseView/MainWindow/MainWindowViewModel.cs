using OsuDatabaseControl.IO.Readers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.DataAccess;
using Test.DataTypes.Osu;
using Test.DTO;


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

        public void LoadData()
        {
            Scores scores = ScoresDBReader.ReadScores("C:\\Users\\kamil\\source\\repos\\OsuDatabaseManager\\OsuDatabaseControl\\scores.db");
            Beatmaps beatmaps = OsuDBReader.ReadBeatmaps("C:\\Users\\kamil\\source\\repos\\OsuDatabaseManager\\OsuDatabaseControl\\osu!.db");
            Scores = new ObservableCollection<ScoreAndBeatmapPrintable>();
            BeatmapDictionary beatmapDictionary = new BeatmapDictionary(beatmaps);
            foreach (Score sc in scores.GetScores())
            {
                Scores.Add(new ScoreAndBeatmapPrintable(sc, beatmapDictionary));
            }

        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
