using System;
using UnityEngine;
public static class ArrayExtensions
{
    public static T Random<T>(this T[] array)
    {
        if (array.Length == 0)
        {
            throw new ArgumentException("Array length is equal to 0.");
        }

        return array[UnityEngine.Random.Range(0, array.Length)];
    }

    public static T Next<T>(this T[] array, T current)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i].Equals(current))
            {
                if (i == array.Length - 1)
                {
                    return array[0];
                }

                return array[++i];
            }
        }

        throw new ArgumentException("Current array item is not valid.");
    }

    public static T Previous<T>(this T[] array, T current)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i].Equals(current))
            {
                if (i == 0)
                {
                    return array.Last();
                }

                return array[--i];
            }
        }

        throw new ArgumentException("Current array item is not valid.");
    }

    public static T Last<T>(this T[] array)
    {
        return array[array.Length - 1];
    }
}
