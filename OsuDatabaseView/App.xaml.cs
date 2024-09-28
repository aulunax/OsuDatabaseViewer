using OsuDatabaseView;
using OsuDatabaseView.MainWindow;
using OsuDatabaseView.StartWindow;
using System.Globalization;
using System.Windows;
using OsuDatabaseControl.Config;

namespace OsuDatabaseView
{
    public partial class App : System.Windows.Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Use dots as decimal point separator
            CultureInfo customCulture = (CultureInfo)CultureInfo.InvariantCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = customCulture;
            Thread.CurrentThread.CurrentUICulture = customCulture;
            CultureInfo.DefaultThreadCurrentCulture = customCulture;
            CultureInfo.DefaultThreadCurrentUICulture = customCulture;
            
            // Add text dictionary
            ResourceDictionary dict = new ResourceDictionary();
            dict.Source = new Uri("..\\Resources\\TextResources.xaml", UriKind.Relative);
            Current.Resources.MergedDictionaries.Clear();
            Current.Resources.MergedDictionaries.Add(dict);

            
            // Load configuration at startup
            ConfigManager.Instance.Config = ConfigManager.Instance.Config;

            if (ConfigManager.Instance.IsValidOsuPath() && ConfigManager.Instance.Config.AutoStart == true)
            {
                MainWindow = new MainWindow.MainWindow();
            }
            else
            {
                MainWindow = new StartWindow.StartWindow();
            }
            MainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            ConfigManager.Instance.SaveConfig();
        }

    }
}