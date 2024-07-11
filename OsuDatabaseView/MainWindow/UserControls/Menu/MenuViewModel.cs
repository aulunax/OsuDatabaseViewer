using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using OsuDatabaseControl.Config;
using CommunityToolkit.Mvvm.Input;
using OsuDatabaseControl.Enums.Display;
using OsuDatabaseControl.IO.Writers;
using OsuDatabaseView.Utils.Dialogs;

namespace OsuDatabaseView.MainWindow.UserControls.Menu
{
    public class MenuViewModel : INotifyPropertyChanged
    {
        private MainWindowViewModel _mainWindowViewModel;

        public MainWindowViewModel MainWindowViewModel
        {
            get => _mainWindowViewModel;
            set
            {
                _mainWindowViewModel = value;
                OnPropertyChanged(nameof(MainWindowViewModel));
            }
        }
        
        public ICommand ChangeAutoStartStateCommand { get; private set; }
        public ICommand ChangeShowSideScoreInfoStateCommand { get; private set; }
        public ICommand ChangeMainColumnVisibilityStateCommand { get; private set; }


        public ICommand SaveScoresAsJsonCommand { get; private set; }
        public ICommand SaveScoresAsXmlCommand { get; private set; }
        public ICommand SaveScoresAsRawTextCommand { get; private set; }

        public void SaveScoresAsJson()
        {
            ScoresToFileSaver.SaveFile(".json","OsuDBManagerScores", FullScoresWriter.WriteToJSON, MainWindowViewModel.FilteredScores);
        }
        
        public void SaveScoresAsXml()
        {
            ScoresToFileSaver.SaveFile(".xml","OsuDBManagerScores", FullScoresWriter.WriteToXML, MainWindowViewModel.FilteredScores);
        }
        
        public void SaveScoresAsRawText()
        {
            ScoresToFileSaver.SaveFile(".txt","OsuDBManagerScores", FullScoresWriter.WriteToText, MainWindowViewModel.FilteredScores);
        }

        public MainColumnVisibility MainColumnVisibilityState
        {
            get { return ConfigManager.Instance.Config.MainColumnVisibility; }
            set
            {
                if (ConfigManager.Instance.Config.MainColumnVisibility == value) return;
                ConfigManager.Instance.Config.MainColumnVisibility = value;
                MainWindowViewModel.MainColumnVisibility = value;
                OnPropertyChanged(nameof(MainColumnVisibilityState));
            }
        }
        
        public bool AutoStartState
        {
            get { return ConfigManager.Instance.Config.AutoStart; }
            set
            {
                ConfigManager.Instance.Config.AutoStart = value;
                OnPropertyChanged(nameof(AutoStartState));
            }
        }
        
        public bool SideScoreInfoState
        {
            get { return ConfigManager.Instance.Config.IsSideScoreInfoShown; }
            set
            {
                ConfigManager.Instance.Config.IsSideScoreInfoShown = value;
                MainWindowViewModel.IsSideScoreInfoVisible = value;
                OnPropertyChanged(nameof(SideScoreInfoState));
            }
        }

        private void ChangeAutoStartState()
        {
            AutoStartState = !AutoStartState;
            ConfigManager.Instance.SaveConfig();
        }
        
        private void ChangeMainColumnVisibilityState(string mask)
        {
            if (!Enum.TryParse<MainColumnVisibility>(mask, out MainColumnVisibility maskEnum)) return;
            if ((MainColumnVisibilityState ^ maskEnum) == 0) return;
            if (maskEnum == MainColumnVisibility.Default) MainColumnVisibilityState = maskEnum;
            else MainColumnVisibilityState ^= maskEnum;
            ConfigManager.Instance.SaveConfig();
        }
        
        private void ChangeShowSideScoreInfoState()
        {
            SideScoreInfoState = !SideScoreInfoState;
            ConfigManager.Instance.SaveConfig();
        }

        public MenuViewModel()
        {
            ChangeMainColumnVisibilityStateCommand = new RelayCommand<string>(ChangeMainColumnVisibilityState);
            ChangeAutoStartStateCommand = new RelayCommand(ChangeAutoStartState);
            ChangeShowSideScoreInfoStateCommand = new RelayCommand(ChangeShowSideScoreInfoState);
            SaveScoresAsJsonCommand = new RelayCommand(SaveScoresAsJson);
            SaveScoresAsXmlCommand = new RelayCommand(SaveScoresAsXml);
            SaveScoresAsRawTextCommand = new RelayCommand(SaveScoresAsRawText);
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
