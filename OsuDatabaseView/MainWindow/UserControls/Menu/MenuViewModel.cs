using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using OsuDatabaseControl.Config;
using CommunityToolkit.Mvvm.Input;

namespace OsuDatabaseView.MainWindow.UserControls.Menu
{
    public class MenuViewModel : INotifyPropertyChanged
    {
        public ICommand ChangeAutoStartStateCommand { get; private set; }

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
