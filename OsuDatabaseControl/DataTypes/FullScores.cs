using System.Diagnostics;
using OsuDatabaseControl.Config;
using OsuDatabaseControl.DataAccess;
using OsuDatabaseControl.DataTypes.Osu;
using OsuDatabaseControl.IO.Readers;
using OsuDatabaseControl.Utils;

namespace OsuDatabaseControl.DataTypes;

public class FullScores
{
    private List<FullScore> fullScores = new List<FullScore>();

    public List<FullScore> GetFullScores()
    {
        return fullScores;
    }

    public void LoadDataFromFile(string osuPath)
    {
        if (!Directory.Exists(osuPath))
        {
            throw new Exception("osu! directory doesnt exist at given path");
        }
                
        OsuDBInfo osuDBInfo = OsuDBReader.ReadOsuDBInfo(Path.Combine(osuPath, FilePaths.OSU_OSUDB_FILENAME));
        Scores scores = ScoresDBReader.ReadScores(Path.Combine(osuPath, FilePaths.OSU_SCOREDB_FILENAME));
        Beatmaps beatmaps = OsuDBReader.ReadBeatmaps(Path.Combine(osuPath, FilePaths.OSU_OSUDB_FILENAME));
        BeatmapDictionary beatmapDictionary = new BeatmapDictionary(beatmaps);

        ConfigManager.Instance.Config.Username = osuDBInfo.PlayerName;
        
        IEnumerable<Score> playerScores = scores.GetScores();
        fullScores.Capacity = playerScores.Count();
        
        foreach (Score sc in playerScores)
        {
            try
            {
                fullScores.Add(new FullScore(sc, beatmapDictionary));
            }
            catch (KeyNotFoundException ex) {
                Debug.WriteLine($"Beatmap Hash is not present in the dictionary: {ex.Message}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception occurred in when adding a Score in MainWindowViewModel: {ex.Message}");
            }
        }
    }
}