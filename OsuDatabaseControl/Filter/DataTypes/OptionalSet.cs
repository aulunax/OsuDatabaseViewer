// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

namespace OsuDatabaseControl.Filter.DataTypes;

public readonly struct OptionalSet<T> : IEquatable<OptionalSet<T>>
    where T : struct, Enum
{
    public bool HasFilter => true;

    public bool IsInRange(T value) => Values.Contains(value);

    public HashSet<T> Values { get; }

    public OptionalSet()
    {
        Values = Enum.GetValues<T>().ToHashSet();
    }

    public bool Equals(OptionalSet<T> other) => Values.SetEquals(other.Values);
}