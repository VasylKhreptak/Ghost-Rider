using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Object = UnityEngine.Object;

public static class NetworkConnection
{
    private class NetworkConnectionMonoBehaviour : MonoBehaviour
    {
    }

    private static NetworkConnectionMonoBehaviour _networkConnection;

    private static void TryInit()
    {
        if (_networkConnection == null)
        {
            GameObject behaviourObject = new GameObject("NetworkConnectionMonoBehaviour");
            behaviourObject.transform.SetParent(null);
            Object.DontDestroyOnLoad(behaviourObject);
            _networkConnection = behaviourObject.AddComponent<NetworkConnectionMonoBehaviour>();
        }
    }

    public static void CheckAsync(Action<bool> result)
    {
        TryInit();

        _networkConnection.StartCoroutine(CheckAsyncRoutine(result));
    }

    private static IEnumerator CheckAsyncRoutine(Action<bool> result)
    {
        const string echoServer = "http://google.com";

        using (UnityWebRequest webRequests = UnityWebRequest.Head(echoServer))
        {
            webRequests.timeout = 1;

            yield return webRequests.SendWebRequest();

            bool hasConnection = webRequests.result != UnityWebRequest.Result.ConnectionError &&
                webRequests.result != UnityWebRequest.Result.ProtocolError &&
                webRequests.result != UnityWebRequest.Result.DataProcessingError &&
                webRequests.responseCode == 200;

            result.Invoke(hasConnection);
        }
    }
}
