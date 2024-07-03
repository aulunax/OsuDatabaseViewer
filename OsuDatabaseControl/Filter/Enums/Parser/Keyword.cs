// Code in this file is a copy/modified copy of code originally copyrighted to Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENSE_ppy file in the repository root for full licence text.

namespace OsuDatabaseControl.Filter.Enums.Parser;

public enum Keyword
{
    artist,
    creator,
    title,
    difficulty,
    star,
    stars,
    mode,
    status,
    ar,
    cs,
    od,
    hp,
    length,
    lastplayed,
    mod,
    mods,
    
    // Added for OsuDatabaseManager
    
    C300,
    C100,
    C50,
    miss,
    score,
    totalscore,
    combo,
    maxcombo
}