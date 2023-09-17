using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

using Random = UnityEngine.Random;

public static class Extensions
{
    public static T Choose<T>(this T[] array, out int index)
    {
        if(array.Length == 0)
        {
            index = -1;
            return default;
        }

        index = Random.Range(0, array.Length);
        return array[index];
    }

    public static T Choose<T>(this T[] array)
    {
        return array.Choose(out int _);
    }

    public static T Choose<T>(this List<T> list, out int index)
    {
        if(list.Count == 0)
        {
            index = -1;
            return default;
        }

        index = Random.Range(0, list.Count);
        return list[index];
    }

    public static T Choose<T>(this List<T> list)
    {
        return list.Choose(out int _);
    }

    public static List<T2> GetOrAdd<T1,T2>(this Dictionary<T1, List<T2>> dict, T1 key)
    {
        if(!dict.ContainsKey(key))
        {
            dict.Add(key, new List<T2>());
        }

        return dict[key];
    }
}

public class Distribution<T>
{
    private Dictionary<Range, T> _ranges;
    private Dictionary<T, int> _distribution;

    internal Distribution(Dictionary<T, int> distribution)
    {
        _distribution = distribution;
        _ranges = DistributionToRanges(_distribution);
    }

    [Pure]
    private static Dictionary<Range, T> DistributionToRanges(Dictionary<T, int> distribution)
    {
        Dictionary<Range, T> ranges = new();

        int total = 0;
        foreach(var kv in distribution)
        {
            var min = total;
            total += kv.Value;
            var max = total;
            ranges.Add(new Range(min, max), kv.Key);
        }

        return ranges;
    }

    public T NextValue()
    {
        int max = _ranges.Max(kv => kv.Key.Max);
        int selection = Random.Range(0, max);

        return _ranges.First(kv => kv.Key.Min <= selection && kv.Key.Max > selection).Value;
    }
}

internal struct Range
{
    public int Min { get; }
    public int Max { get; }

    public Range(int min, int max)
    {
        Min = min;
        Max = max;
    }
}
