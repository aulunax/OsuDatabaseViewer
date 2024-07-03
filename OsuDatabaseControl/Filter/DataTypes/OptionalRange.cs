// Code in this file is a copy/modified copy of code originally copyrighted to Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENSE_ppy file in the repository root for full licence text.

namespace OsuDatabaseControl.Filter.DataTypes;

public struct OptionalRange<T> : IEquatable<OptionalRange<T>>
    where T : struct
{
    public bool HasFilter => Max != null || Min != null;

    public bool IsInRange(T value)
    {
        if (Min != null)
        {
            int comparison = Comparer<T>.Default.Compare(value, Min.Value);

            if (comparison < 0)
                return false;

            if (comparison == 0 && !IsLowerInclusive)
                return false;
        }

        if (Max != null)
        {
            int comparison = Comparer<T>.Default.Compare(value, Max.Value);

            if (comparison > 0)
                return false;

            if (comparison == 0 && !IsUpperInclusive)
                return false;
        }

        return true;
    }

    public T? Min;
    public T? Max;
    public bool IsLowerInclusive;
    public bool IsUpperInclusive;

    public bool Equals(OptionalRange<T> other)
        => EqualityComparer<T?>.Default.Equals(Min, other.Min)
           && EqualityComparer<T?>.Default.Equals(Max, other.Max)
           && IsLowerInclusive.Equals(other.IsLowerInclusive)
           && IsUpperInclusive.Equals(other.IsUpperInclusive);
}    
