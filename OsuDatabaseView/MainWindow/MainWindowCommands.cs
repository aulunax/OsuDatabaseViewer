using System.Windows.Input;

namespace OsuDatabaseView.MainWindow;

public static class MainWindowCommands
{
    public static RoutedCommand FocusSearchBox = new RoutedCommand();
    
    public static void InitilizeKeyboardShortcuts()
    {
        FocusSearchBox.InputGestures.Add(new KeyGesture(Key.F, ModifierKeys.Control));
    }

}