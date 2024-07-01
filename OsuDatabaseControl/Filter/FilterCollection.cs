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
                term.Matches(s.DifficultyName)
                );
        }
        
        collection = collection.Where(s => criteria.DifficultyName.Matches(s.DifficultyName));
        collection = collection.Where(s => criteria.Creator.Matches(s.CreatorName));
        collection = collection.Where(s => criteria.Title.Matches(s.SongTitle) || criteria.Title.Matches(s.SongTitleUnicode));
        collection = collection.Where(s => criteria.Artist.Matches(s.ArtistName) || criteria.Artist.Matches(s.ArtistNameUnicode));
        collection = collection.Where(s => criteria.LastPlayed.IsInRange(s.Date));
        collection = collection.Where(s => criteria.StarDifficulty.IsInRange(s.StarRating));
        collection = collection.Where(s => criteria.OnlineStatus.IsInRange(s.RankedStatus));
        collection = collection.Where(s => criteria.Mods.IsInRange(s.Mods));



    }
}