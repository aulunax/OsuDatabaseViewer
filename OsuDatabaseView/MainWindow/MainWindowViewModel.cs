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
using System.Windows.Input;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.Input;
using OsuDatabaseControl.Config;
using OsuDatabaseControl.DataAccess;
using OsuDatabaseControl.DataTypes;
using OsuDatabaseControl.DataTypes.Osu;
using OsuDatabaseControl.DTO;
using OsuDatabaseControl.Enums.Display;
using OsuDatabaseControl.Filter;


namespace OsuDatabaseView.MainWindow
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private DispatcherTimer _debounceTimer;
        
        private ObservableCollection<FullScore> _filteredScores;
        private bool _isSideScoreInfoVisible;

        private FullScores _originalScores = new FullScores();

        private string _searchBoxQuery;

        private FullScore? _selectedScoreInfo;

        private string _totalNumberOfDisplayedScores;

        public MainWindowViewModel()
        {
            MainColumnVisibility = ConfigManager.Instance.Config.MainColumnVisibility;
            UpdateFilteredScoresCommand = new RelayCommand(UpdateFilteredScores);
            SelectionChangedCommand = new RelayCommand(SelectionChanged);
            ShowSelectedBeatmapsetCommand = new RelayCommand<FullScore>(ShowSelectedBeatmapset);
            ShowSelectedBeatmapDifficultyCommand = new RelayCommand<FullScore>(ShowSelectedBeatmapDifficulty);
            OpenBeatmapInNotepadCommand = new RelayCommand<FullScore>(OpenBeatmapInNotepad);
            ChangeVisibilityCommand = new RelayCommand<string>(ChangeVisibility);
            
            _debounceTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(200)
            };
            
            _debounceTimer.Tick += (sender, args) =>
            {
                _debounceTimer.Stop();
                UpdateFilteredScores();
            };           
            
            LoadData();
        }



        private void OpenBeatmapInNotepad(FullScore score)
        {
            string path = Path.Combine(ConfigManager.Instance.Config.OsuDirectory, "Songs", score.FolderName, score.OsuFileName);
            Process.Start("notepad.exe", path);
        }

        public ICommand ShowSelectedBeatmapsetCommand { get; set; }
        public ICommand ShowSelectedBeatmapDifficultyCommand { get; set; }
        public ICommand OpenBeatmapInNotepadCommand { get; set; }

        
        private void ShowSelectedBeatmapDifficulty(FullScore score)
        {
            if (score.BeatmapId == 0)
                SearchBoxQuery = $"hash={score.MD5Hash}";
            else
                SearchBoxQuery = $"diffid={score.BeatmapId}";
        }

        public MainColumnVisibility MainColumnVisibility
        {
            get => ConfigManager.Instance.Config.MainColumnVisibility;
            set
            {
                if (ConfigManager.Instance.Config.MainColumnVisibility != value)
                {
                    ConfigManager.Instance.Config.MainColumnVisibility = value;
                }
                OnPropertyChanged(nameof(MainColumnVisibility));
            }
        }
        
        public ICommand ChangeVisibilityCommand { get; set; }
        
        private void ChangeVisibility(string mask)
        {
            if (!Enum.TryParse<MainColumnVisibility>(mask, out MainColumnVisibility maskEnum)) return;
            if ((MainColumnVisibility ^ maskEnum) == 0) return;
            if (maskEnum == MainColumnVisibility.Default) MainColumnVisibility = maskEnum;
            else MainColumnVisibility ^= maskEnum;
            ConfigManager.Instance.SaveConfig();
        }

        public ObservableCollection<FullScore> FilteredScores
        {
            get { return _filteredScores; }
            set
            {
                _filteredScores = value;
                TotalNumberOfDisplayedScores = _filteredScores.Count().ToString();
                OnPropertyChanged(nameof(FilteredScores));
            }
        }

        public bool IsSideScoreInfoVisible
        {
            get { return _isSideScoreInfoVisible; }
            set
            {
                _isSideScoreInfoVisible = value;
                OnPropertyChanged(nameof(IsSideScoreInfoVisible));
            }
        }

        public ICommand SelectionChangedCommand { get; set; }

        public FullScore? SelectedScoreInfo
        {
            get => _selectedScoreInfo;
            set
            {
                if (_selectedScoreInfo != value)
                {
                    _selectedScoreInfo = value;
                    OnPropertyChanged(nameof(SelectedScoreInfo));
                    SelectionChangedCommand.Execute(_selectedScoreInfo);
                    if (ConfigManager.Instance.Config.IsSideScoreInfoShown && _selectedScoreInfo is not null)
                        IsSideScoreInfoVisible = true;
                    else
                        IsSideScoreInfoVisible = false;
                }
            }
        }

        public string TotalNumberOfDisplayedScores
        {
            get => _totalNumberOfDisplayedScores;
            set
            {
                if (_totalNumberOfDisplayedScores != value)
                {
                    _totalNumberOfDisplayedScores = value;
                    OnPropertyChanged(nameof(TotalNumberOfDisplayedScores));
                }
            }
        }

        public string SearchBoxQuery
        {
            get => _searchBoxQuery;
            set
            {
                if (_searchBoxQuery != value)
                {
                    _searchBoxQuery = value;
                    _debounceTimer.Stop();
                    _debounceTimer.Start();
                    OnPropertyChanged(nameof(SearchBoxQuery));
                }
            }
        }

        public ICommand UpdateFilteredScoresCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void SelectionChanged()
        {
        }

        private void ShowSelectedBeatmapset(FullScore score)
        {
            SearchBoxQuery = $"id={score.BeatmapSetId}";
        }

        private async Task LoadData()
        {
            try
            {
                await Task.Run(() =>
                {
                    _originalScores.LoadDataFromFile(ConfigManager.Instance.Config.OsuDirectory);
                });
                FilteredScores = new ObservableCollection<FullScore>(_originalScores.GetFullScores());
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception occurred in LoadData: {ex.Message}");
            }
        }

        private void UpdateFilteredScores()
        {
            FilterCriteria criteria = new FilterCriteria();
            FilterParser.ApplyQueries(criteria, SearchBoxQuery);
            
            IEnumerable<FullScore> filteredEnumerable = _originalScores.GetFullScores();
            FilterCollection.Filter(ref filteredEnumerable, criteria);
            
            FilteredScores = new ObservableCollection<FullScore>(filteredEnumerable);
            
        }

        private void OnSelectedScoreInfoChanged()
        {
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}