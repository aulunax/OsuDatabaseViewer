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
using CommunityToolkit.Mvvm.Input;
using OsuDatabaseControl.Config;
using OsuDatabaseControl.DataAccess;
using OsuDatabaseControl.DataTypes;
using OsuDatabaseControl.DataTypes.Osu;
using OsuDatabaseControl.DTO;
using OsuDatabaseControl.Filter;


namespace OsuDatabaseView.MainWindow
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<FullScore> _filteredScores;
        private FullScores _originalScores = new FullScores(); 

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
        private bool _isSideScoreInfoVisible;
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
        private void SelectionChanged() {}
        
        private FullScore? _selectedScoreInfo;
        
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

        private string _totalNumberOfDisplayedScores;

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
        
        private string _searchBoxQuery;

        public string SearchBoxQuery
        {
            get => _searchBoxQuery;
            set
            {
                if (_searchBoxQuery != value)
                {
                    _searchBoxQuery = value;
                    OnPropertyChanged(nameof(SearchBoxQuery));
                }
            }
        }

        public ICommand UpdateFilteredScoresCommand { get; private set; }
        
        public MainWindowViewModel()
        {
            UpdateFilteredScoresCommand = new RelayCommand(UpdateFilteredScores);
            SelectionChangedCommand = new RelayCommand(SelectionChanged);
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                _originalScores.LoadDataFromFile(ConfigManager.Instance.Config.OsuDirectory);
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

            var filteredEnumerable = _originalScores.GetFullScores().AsEnumerable(); 
            FilterCollection.Filter(ref filteredEnumerable, criteria);
            
            FilteredScores = new ObservableCollection<FullScore>(filteredEnumerable);
        }

        private void OnSelectedScoreInfoChanged()
        {
            
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
