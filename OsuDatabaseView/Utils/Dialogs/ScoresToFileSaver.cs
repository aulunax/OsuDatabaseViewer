using System.Diagnostics;

namespace OsuDatabaseView.Utils.Dialogs;

public class ScoresToFileSaver
{
    public static void SaveFile<T>(string defaultExt, string fileName, Func<T, string, bool> saveFunction, T data)
    {
        string? savePath = null;
        using (var dialog = new SaveFileDialog())
        {
            dialog.DefaultExt = defaultExt;
            dialog.AddExtension = true;
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            dialog.FileName = fileName;
            dialog.Filter = $"{defaultExt} files (*{defaultExt})|*{defaultExt}|All files (*.*)|*.*";

            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.FileName))
            {
                savePath = dialog.FileName;
            }
        }

        if (savePath is null)
        {
            Debug.WriteLine($"File selection dialog failure in SaveFile method");
            return;
        }

        bool success = saveFunction(data, savePath);

        if (!success)
        {
            MessageBox.Show($"Couldn't save the file at {savePath}", "Saving Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}