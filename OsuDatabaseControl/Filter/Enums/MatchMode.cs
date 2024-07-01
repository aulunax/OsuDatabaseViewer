// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace OsuDatabaseControl.Filter.Enums;

public enum MatchMode
{
    /// <summary>
    /// Match using a simple "contains" substring match.
    /// </summary>
    Substring,

    /// <summary>
    /// Match for the search phrase being isolated by spaces, or at the start or end of the text.
    /// </summary>
    IsolatedPhrase,

    /// <summary>
    /// Match for the search phrase matching the full text in completion.
    /// </summary>
    FullPhrase,
}