using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;


public static class Extensions
{
    public static T With<T>(this T type, Action<T> action)
    {
        action.Invoke(type);
        return type;
    }

    public static T With<T>(this T type, Action action)
    {
        action.Invoke();
        return type;
    }

    public static T With<T>(this T type, Func<T, T> action)
    {
        return action.Invoke(type);
    }

    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> type, Action<T> action)
    {
        foreach (var item in type)
        {
            action?.Invoke(item);
        }

        return type;
    }

    public static IReadOnlyCollection<T> ForEach<T>(this IReadOnlyCollection<T> type, Action<T> action)
    {
        foreach (var item in type)
        {
            action?.Invoke(item);
        }

        return type;
    }

    public static IEnumerable<T> For<T>(this IEnumerable<T> type, Action<T> action)
    {
        for (int i = 0; i < type.Count(); i++)
        {
            action?.Invoke(type.ElementAt(i));
        }

        return type;
    }

    public static IEnumerable<T> For<T>(this IEnumerable<T> type, Action<T, int> action)
    {
        for (int i = 0; i < type.Count(); i++)
        {
            action?.Invoke(type.ElementAt(i), i);
        }

        return type;
    }

    public static IReadOnlyCollection<T> For<T>(this IReadOnlyCollection<T> type, Action<T> action)
    {
        for (int i = 0; i < type.Count(); i++)
        {
            action?.Invoke(type.ElementAt(i));
        }

        return type;
    }

    public static IReadOnlyCollection<T> For<T>(this IReadOnlyCollection<T> type, Action<T, int> action)
    {
        for (int i = 0; i < type.Count(); i++)
        {
            action?.Invoke(type.ElementAt(i), i);
        }

        return type;
    }
}
