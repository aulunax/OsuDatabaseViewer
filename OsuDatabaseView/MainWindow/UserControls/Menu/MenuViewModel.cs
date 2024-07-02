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
        
        
        
        public bool AutoStartState
        {
            get { return ConfigManager.Instance.Config.AutoStart; }
            set
            {
                ConfigManager.Instance.Config.AutoStart = value;
                OnPropertyChanged(nameof(AutoStartState));
            }
        }



        public MenuViewModel()
        {
            ChangeAutoStartStateCommand = new RelayCommand(ChangeAutoStartState);
            SaveScoresAsJsonCommand = new RelayCommand(SaveScoresAsJson);
            SaveScoresAsXmlCommand = new RelayCommand(SaveScoresAsXml);
            SaveScoresAsRawTextCommand = new RelayCommand(SaveScoresAsRawText);
        }

        private void ChangeAutoStartState()
        {
            AutoStartState = !AutoStartState;
            ConfigManager.Instance.SaveConfig();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
