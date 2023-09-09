using System.Collections.Generic;

using UnityEngine;

public static class Extensions
{
    public static T Choose<T>(this T[] array)
    {
        if(array.Length == 0) return default;

        int index = Random.Range(0, array.Length);
        return array[index];
    }

    public static T Choose<T>(this List<T> list)
    {
        if(list.Count == 0) return default;

        int index = Random.Range(0, list.Count);
        return list[index];
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
