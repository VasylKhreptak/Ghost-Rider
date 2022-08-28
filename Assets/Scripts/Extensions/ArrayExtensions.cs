public static class ArrayExtensions
{
    public static T Random<T>(this T[] array)
    {
        return array[UnityEngine.Random.Range(0, array.Length)];
    }

    public static T GetNext<T>(this T[] array, T current)
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

        return array[0];
    }
}