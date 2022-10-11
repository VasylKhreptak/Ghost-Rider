using System;
using UnityEngine;

public static class PlayerPrefsSafeDataProvider
{
    public static void Save<T>(string key, T obj)
    {
        string saveJson = JsonUtility.ToJson(obj);

        PlayerPrefsSafe.SetString(key, saveJson);
    }

    public static T Load<T>(string key)
    {
        if (PlayerPrefsSafe.HasKey(key) == false)
        {
            throw new ArgumentException("There is not key of name: " + (key));
        }

        string saveJson = PlayerPrefsSafe.GetString(key);

        return JsonUtility.FromJson<T>(saveJson);
    }

    public static T Load<T>(string key, T @default)
    {
        return PlayerPrefsSafe.HasKey(key) == false ? @default : Load<T>(key);
    }
}