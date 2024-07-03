// Code in this file is a copy/modified copy of code originally copyrighted to Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENSE_ppy file in the repository root for full licence text.

using System.ComponentModel;
using OsuDatabaseControl.DataTypes.Osu;

namespace OsuDatabaseControl.Filter.DataTypes;

public readonly struct OptionalSet<T> : IEquatable<OptionalSet<T>>
    where T : struct, Enum
{
    public bool HasFilter => true;

    public bool IsInRange(T value) => Values.Contains(value);

    public bool IsInRangeFlags(T value)
    {
        int hashValues = 0;
        foreach (var val in Values)
        {
            hashValues |= Convert.ToInt32(val);
        }

        if (hashValues == 0 || hashValues == Int32.MaxValue)
            return true;

        return (hashValues & Convert.ToInt32(value)) != 0;
    }


    public HashSet<T> Values { get; }

    public OptionalSet()
    {
        Values = Enum.GetValues<T>().ToHashSet();
    }

    public bool Equals(OptionalSet<T> other) => Values.SetEquals(other.Values);
}