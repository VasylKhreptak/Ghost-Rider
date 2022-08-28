using System;
using UnityEngine;
using Object = UnityEngine.Object;

public static class GameObjectExtensions 
{
    public static bool IsValid(this GameObject gameObject)
    {
        return gameObject != null && gameObject.activeSelf;
    }

    public static bool IsNotValid(this GameObject gameObject)
    {
        return gameObject == null || gameObject.activeSelf == false;
    }

    public static GameObject InstantiateDontDestroyOnLoad(GameObject gameObject)
    {
        GameObject instance = Object.Instantiate(gameObject);

        instance.transform.SetParent(null);
        
        Object.DontDestroyOnLoad(instance);

        return instance;
    }
}
