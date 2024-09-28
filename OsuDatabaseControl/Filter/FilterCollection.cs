using OsuDatabaseControl.DataTypes;

namespace OsuDatabaseControl.Filter;

public class FilterCollection
{
    public static void Filter(ref IEnumerable<FullScore> collection, FilterCriteria criteria)
    {
        foreach (var term in criteria.SearchTerms)
        {
            collection = collection.Where(s => 
                term.Matches(s.ArtistName) || 
                term.Matches(s.SongTitle) || 
                term.Matches(s.CreatorName) || 
                term.Matches(s.DifficultyName) ||
                term.Matches(s.SongTags) ||
                term.Matches(s.BeatmapId.ToString()) ||
                term.Matches(s.BeatmapSetId.ToString())
                );
        }
        
        if (criteria.DifficultyName.HasFilter)
            collection = collection.Where(s => criteria.DifficultyName.Matches(s.DifficultyName));
        if (criteria.Creator.HasFilter)
            collection = collection.Where(s => criteria.Creator.Matches(s.CreatorName));
        if (criteria.Title.HasFilter)
            collection = collection.Where(s => criteria.Title.Matches(s.SongTitle) || criteria.Title.Matches(s.SongTitleUnicode));
        if (criteria.Artist.HasFilter)
            collection = collection.Where(s => criteria.Artist.Matches(s.ArtistName) || criteria.Artist.Matches(s.ArtistNameUnicode));
        if (criteria.LastPlayed.HasFilter)
            collection = collection.Where(s => criteria.LastPlayed.IsInRange(s.Date));
        if (criteria.StarDifficulty.HasFilter)
            collection = collection.Where(s => criteria.StarDifficulty.IsInRange(s.StarRating));
        if (criteria.OnlineStatus.HasFilter)
            collection = collection.Where(s => criteria.OnlineStatus.IsInRange(s.RankedStatus));
        if (criteria.ApproachRate.HasFilter)
            collection = collection.Where(s => criteria.ApproachRate.IsInRange(s.ApproachRate));
        if (criteria.CircleSize.HasFilter)
            collection = collection.Where(s => criteria.CircleSize.IsInRange(s.CircleSize));
        if (criteria.OverallDifficulty.HasFilter)
            collection = collection.Where(s => criteria.OverallDifficulty.IsInRange(s.OverallDifficulty));
        if (criteria.DrainRate.HasFilter)
            collection = collection.Where(s => criteria.DrainRate.IsInRange(s.HPDrain));
        if (criteria.Length.HasFilter)
            collection = collection.Where(s => criteria.Length.IsInRange(s.TotalTime));
        
        // Added filters for OsuDatabaseManager:
        
        if (criteria.Mods.HasFilter)
            collection = collection.Where(s => criteria.Mods.IsInRangeFlags(s.Mods));   
        if (criteria.PlayMode.HasFilter)
            collection = collection.Where(s => criteria.PlayMode.IsInRange(s.PlayMode));
        if (criteria.C300Count.HasFilter)
            collection = collection.Where(s => criteria.C300Count.IsInRange(s.C300));
        if (criteria.C100Count.HasFilter)
            collection = collection.Where(s => criteria.C100Count.IsInRange(s.C100));
        if (criteria.C50Count.HasFilter)
            collection = collection.Where(s => criteria.C50Count.IsInRange(s.C50));
        if (criteria.MissCount.HasFilter)
            collection = collection.Where(s => criteria.MissCount.IsInRange(s.Miss));
        if (criteria.TotalScore.HasFilter)
            collection = collection.Where(s => criteria.TotalScore.IsInRange(s.TotalScore));
        if (criteria.Combo.HasFilter)
            collection = collection.Where(s => criteria.Combo.IsInRange(s.MaxCombo));
        if (criteria.Bpm.HasFilter)
            collection = collection.Where(s => criteria.Bpm.IsInRange(s.BPM));
        if (criteria.Accuracy.HasFilter)
            collection = collection.Where(s => criteria.Accuracy.IsInRange(s.Accuracy*100));
        if (criteria.PlayerName.HasFilter)
            collection = collection.Where(s => criteria.PlayerName.Matches(s.PlayerName));
        if (criteria.BeatmapID.HasFilter)
            collection = collection.Where(s => criteria.BeatmapID.IsInRange(s.BeatmapId));
        if (criteria.BeatmapsetID.HasFilter)
            collection = collection.Where(s => criteria.BeatmapsetID.IsInRange(s.BeatmapSetId));        
        if (criteria.MD5Hash.HasFilter)
            collection = collection.Where(s => criteria.MD5Hash.Matches(s.MD5Hash));
    }
}