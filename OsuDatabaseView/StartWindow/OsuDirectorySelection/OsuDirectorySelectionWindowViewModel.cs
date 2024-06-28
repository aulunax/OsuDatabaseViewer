using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace OsuDatabaseView.StartWindow.OsuDirectorySelection
{
    public class OsuDirectorySelectionWindowViewModel : INotifyPropertyChanged
    {
        private IEnumerable<string> _pathsList;

        public IEnumerable<string> PathsList { 
            get { return _pathsList; } 
            set { 
                _pathsList = value;
                OnPropertyChanged(nameof(PathsList));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
