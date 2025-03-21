﻿// Code in this file is a copy/modified copy of code originally copyrighted to Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENSE_ppy file in the repository root for full licence text.

using System.Text.RegularExpressions;
using OsuDatabaseControl.DataTypes.Osu;
using OsuDatabaseControl.Enums;
using OsuDatabaseControl.Filter.DataTypes;

namespace OsuDatabaseControl.Filter;

public class FilterCriteria
{
    
    public OptionalRange<double> StarDifficulty;
    public OptionalRange<float> ApproachRate;
    public OptionalRange<float> DrainRate;
    public OptionalRange<float> CircleSize;
    public OptionalRange<float> OverallDifficulty;
    public OptionalRange<double> Length;
    public OptionalRange<DateTimeOffset> LastPlayed;
    public OptionalTextFilter Creator;
    public OptionalTextFilter Artist;
    public OptionalTextFilter Title;
    public OptionalTextFilter DifficultyName;
    public OptionalSet<BeatmapRankedStatus> OnlineStatus = new OptionalSet<BeatmapRankedStatus>();
    
    // Added filters for OsuDatabaseManager:
    
    public OptionalSet<Mods> Mods = new OptionalSet<Mods>();
    public OptionalSet<PlayMode> PlayMode = new OptionalSet<PlayMode>();
    public OptionalRange<int> Combo;
    public OptionalRange<int> MissCount;
    public OptionalRange<int> C300Count;
    public OptionalRange<int> C100Count;
    public OptionalRange<int> C50Count;
    public OptionalRange<int> TotalScore;
    public OptionalRange<double> Bpm;
    public OptionalRange<double> Accuracy;
    public OptionalTextFilter PlayerName;
    public OptionalTextFilter MD5Hash;

    public OptionalRange<int> BeatmapID;
    public OptionalRange<int> BeatmapsetID;



    
    
    public OptionalTextFilter[] SearchTerms = Array.Empty<OptionalTextFilter>();
    public bool AllowConvertedBeatmaps;
    
    private string searchText = string.Empty;

        /// <summary>
        /// <see cref="SearchText"/> as a number (if it can be parsed as one).
        /// </summary>
        public int? SearchNumber { get; private set; }

        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;

                List<OptionalTextFilter> terms = new List<OptionalTextFilter>();

                string remainingText = value;

                // Match either an open difficulty tag to the end of string,
                // or match a closed one with a whitespace after it.
                //
                // To keep things simple, the closing ']' may be included in the match group,
                // and is trimmed post-match.
                foreach (Match quotedSegment in Regex.Matches(value, "(^|\\s)\\[(.*)(\\]\\s|$)"))
                {
                    DifficultyName = new OptionalTextFilter
                    {
                        SearchTerm = quotedSegment.Groups[2].Value.Trim(']')
                    };

                    remainingText = remainingText.Replace(quotedSegment.Value, string.Empty);
                }

                // First handle quoted segments to ensure we keep inline spaces in exact matches.
                foreach (Match quotedSegment in Regex.Matches(value, "(\"[^\"]+\"[!]?)"))
                {
                    terms.Add(new OptionalTextFilter { SearchTerm = quotedSegment.Value });
                    remainingText = remainingText.Replace(quotedSegment.Value, string.Empty);
                }

                // Then handle the rest splitting on any spaces.
                terms.AddRange(remainingText.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => new OptionalTextFilter
                {
                    SearchTerm = s
                }));

                SearchTerms = terms.ToArray();

                SearchNumber = null;

                if (SearchTerms.Length == 1 && int.TryParse(SearchTerms[0].SearchTerm, out int parsed))
                    SearchNumber = parsed;
            }
        }

}