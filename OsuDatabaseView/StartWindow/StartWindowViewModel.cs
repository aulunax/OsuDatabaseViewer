using CommunityToolkit.Mvvm.Input;
using OsuDatabaseControl.IO;
using OsuDatabaseView.StartWindow.OsuDirectorySelection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace OsuDatabaseView.StartWindow
{
    public class StartWindowViewModel : INotifyPropertyChanged
    {
        private string _selectedPath = null;
        private bool _isAutoDetecting = false;
        private int _autoDetectionProgress = 0;

        public ICommand DetectOsuCommand { get; private set; }
        public ICommand OpenDirectoryCommand { get; private set; }
        public ICommand ConfirmationCommand { get; private set; }

        private void Confirmation()
        {
        }

        public string SelectedPath
        {
            get { return _selectedPath; }
            set
            {
                if (_selectedPath != value)
                {
                    _selectedPath = value;
                    OnPropertyChanged(nameof(SelectedPath));
                }
            }
        }
        public bool IsDetecting
        {
            get { return _isAutoDetecting; }
            set
            {
                _isAutoDetecting = value;
                OnPropertyChanged(nameof(IsDetecting));
            }
        }

        public int AutoDetectionProgress
        {
            get => _autoDetectionProgress;
            set
            {
                _autoDetectionProgress = value;
                OnPropertyChanged(nameof(AutoDetectionProgress));
            }
        }


        public StartWindowViewModel()
        {
            DetectOsuCommand = new RelayCommand(async () => await DetectOsuDirectoryAsync());
            OpenDirectoryCommand = new RelayCommand(() => SelectOsuPathDialog());
            ConfirmationCommand = new RelayCommand(Confirmation);
        }
        private void SelectOsuPathDialog()
        {
            IsDetecting = true;
            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
                {
                    SelectedPath = dialog.SelectedPath;
                }
            }
            IsDetecting = false;
        }

        // TODO: Fix this disgusting mess
        private async Task DetectOsuDirectoryAsync()
        {
            IsDetecting = true;
            Progress<int> progress = new Progress<int>(value => AutoDetectionProgress = value);
            ((IProgress<int>)progress).Report(0);

            DirectorySearch directorySearch = new DirectorySearch(progress);

            bool found = false;
            List<string> detectedPaths = await Task.Run(() => directorySearch.SearchForOsuDirectory());

            if (detectedPaths.Count > 0)
                found = true;
 
            ((IProgress<int>)progress).Report(100);


            if (found && detectedPaths.Count == 1)
            {
                SelectedPath = detectedPaths.First();
                System.Windows.MessageBox.Show($"Found osu! directory at: {SelectedPath}", "Directory Found", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (found && detectedPaths.Count > 1)
            {
                OsuDirectorySelectionWindow osuDirectorySelectionWindow = new OsuDirectorySelectionWindow();
                osuDirectorySelectionWindow.DataContext = new OsuDirectorySelectionWindowViewModel()
                {
                    PathsList = detectedPaths
                };

                bool? result = osuDirectorySelectionWindow.ShowDialog();

                if (result == true && osuDirectorySelectionWindow.SelectedItem != null)
                {
                    SelectedPath = (string)osuDirectorySelectionWindow.SelectedItem;
                }
                else
                {
                    System.Windows.MessageBox.Show("Selection canceled or window closed.", "Selection canceled", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                System.Windows.MessageBox.Show("osu! directory not found.", "Directory Not Found", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            IsDetecting = false;

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }
}
