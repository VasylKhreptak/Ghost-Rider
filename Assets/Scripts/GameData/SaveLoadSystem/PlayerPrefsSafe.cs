using System;
using System.Globalization;
using UnityCipher;
using UnityEngine;

public static class PlayerPrefsSafe
{
    private static string _password = "&aHB3FcB5vb@Q@s3$H25^WVLfazT$%zuf4&aEAYZphLJGk8*bK%Fk" +
        "Uh?-T*z#WL&Ws=kSajE=Q%JMcp#q%QBX9xQCgRTzUZhv5xzD!zS$k" +
        "8&jkR7b!jChj4WcgL43zH7%^45465423x5%^&$%$#$@23434d";

    public static void SetString(string key, string value)
    {
        string encryptedValue = RijndaelEncryption.Encrypt(value, _password);

        PlayerPrefs.SetString(key, encryptedValue);
    }

    public static string GetString(string key)
    {
        if (PlayerPrefs.HasKey(key) == false)
        {
            throw new ArgumentException("No key: " + (key));
        }

        string decrypted = RijndaelEncryption.Decrypt(PlayerPrefs.GetString(key), _password);

        return decrypted;
    }

    public static string GetString(string key, string @default)
    {
        return HasKey(key) ? GetString(key) : @default;
    }

    public static void SetInt(string key, int value)
    {
        SetString(key, value.ToString());
    }

    public static int GetInt(string key)
    {
        return int.Parse(GetString(key));
    }

    public static int GetInt(string key, int @default)
    {
        return HasKey(key) ? GetInt(key) : @default;
    }

    public static void SetFloat(string key, float value)
    {
        SetString(key, value.ToString(CultureInfo.InvariantCulture));
    }

    public static float GetFloat(string key)
    {
        return float.Parse(GetString(key), CultureInfo.InvariantCulture);
    }

    public static float GetFloat(string key, float @default)
    {
        return HasKey(key) ? GetFloat(key) : @default;
    }

    public static void SetBool(string key, bool value)
    {
        SetString(key, value.ToString());
    }

    public static bool GetBool(string key)
    {
        return bool.Parse(GetString(key));
    }

    public static bool GetBool(string key, bool @default)
    {
        return HasKey(key) ? GetBool(key) : @default;
    }

    public static bool HasKey(string key)
    {
        return PlayerPrefs.HasKey(key);
    }

    public static void DeleteKey(string key)
    {
        PlayerPrefs.DeleteKey(key);
    }
}
