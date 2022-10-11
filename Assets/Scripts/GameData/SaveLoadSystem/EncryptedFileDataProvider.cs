using System;
using UnityEngine;
using UnityCipher;

public static class EncryptedFileDataProvider
{
    private static string _password = "&aHB3FcB5vb@Q@s3$H25^WVLfazT$%zuf4&aEAYZphLJGk8*bK%Fk" +
        "Uh?-T*z#WL&Ws=kSajE=Q%JMcp#q%QBX9xQCgRTzUZhv5xzD!zS$k" +
        "8&jkR7b!jChj4WcgL43zH7%^45465423x5%^&$%$#$@23434d";

    public static void Save<T>(string path, T obj)
    {
        string data = JsonUtility.ToJson(obj);
        string encryptedData = RijndaelEncryption.Encrypt(data, _password);

        System.IO.File.WriteAllText(path, encryptedData);
    }

    public static T Load<T>(string path)
    {
        if (System.IO.File.Exists(path) == false)
        {
            throw new ArgumentException("There is not path: " + (path));
        }

        string encryptedData = System.IO.File.ReadAllText(path);
        string decryptedData = RijndaelEncryption.Decrypt(encryptedData, _password);

        return JsonUtility.FromJson<T>(decryptedData);
    }

    public static T Load<T>(string path, T @default)
    {
        return System.IO.File.Exists(path) == false ? @default : Load<T>(path);
    }
}
