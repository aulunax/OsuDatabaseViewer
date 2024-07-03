// Code in this file is a copy/modified copy of code originally copyrighted to Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENSE_ppy file in the repository root for full licence text.

using System.Globalization;
using System.Text.RegularExpressions;
using OsuDatabaseControl.Filter.Enums;

namespace OsuDatabaseControl.Filter.DataTypes;

public struct OptionalTextFilter : IEquatable<OptionalTextFilter>
{
    public bool HasFilter => !string.IsNullOrEmpty(SearchTerm);

    public MatchMode MatchMode { get; private set; }

    public bool Matches(string value)
    {
        if (!HasFilter)
            return true;

        // search term is guaranteed to be non-empty, so if the string we're comparing is empty, it's not matching
        if (string.IsNullOrEmpty(value))
            return false;

        switch (MatchMode)
        {
            default:
            case MatchMode.Substring:
                // Note that we are using ordinal here to avoid performance issues caused by globalisation concerns.
                // See https://github.com/ppy/osu/issues/11571 / https://github.com/dotnet/docs/issues/18423.
                return value.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase);

            case MatchMode.IsolatedPhrase:
                return Regex.IsMatch(value, $@"(^|\s){Regex.Escape(searchTerm)}($|\s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

            case MatchMode.FullPhrase:
                return CultureInfo.InvariantCulture.CompareInfo.Compare(value, searchTerm, CompareOptions.OrdinalIgnoreCase) == 0;
        }
    }

    private string searchTerm;

    public string SearchTerm
    {
        get => searchTerm;
        set
        {
            searchTerm = value;

            if (searchTerm.StartsWith('\"'))
            {
                // length check ensures that the quote character in the `StartsWith()` check above and the `EndsWith()` check below is not the same character.
                if (searchTerm.EndsWith("\"!", StringComparison.Ordinal) && searchTerm.Length >= 3)
                {
                    searchTerm = searchTerm.TrimEnd('!').Trim('\"');
                    MatchMode = MatchMode.FullPhrase;
                }
                else
                {
                    searchTerm = searchTerm.Trim('\"');
                    MatchMode = MatchMode.IsolatedPhrase;
                }
            }
            else
                MatchMode = MatchMode.Substring;
        }
    }

    public bool Equals(OptionalTextFilter other) => SearchTerm == other.SearchTerm;
}