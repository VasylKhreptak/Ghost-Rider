using System;
using System.Collections;
using System.Threading.Tasks;
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
            webRequests.timeout = 2;

            yield return webRequests.SendWebRequest();

            bool hasConnection = webRequests.result != UnityWebRequest.Result.ConnectionError &&
                webRequests.result != UnityWebRequest.Result.ProtocolError &&
                webRequests.result != UnityWebRequest.Result.DataProcessingError &&
                webRequests.responseCode == 200;

            result.Invoke(hasConnection);
        }
    }

    public static void GetPublicIP(Action<string> onSuccess, Action<string> onError = null)
    {
       WebRequests.Text.GetAsync(new Uri("http://checkip.dyndns.org"), OnGetRequestText, OnError);

       void OnGetRequestText(string text)
       {
           Task<string> task = Task.Run(() => GetIpFromText(text));

           string GetIpFromText(string test)
           {
               string[] a = text.Split(':');
               string a2 = a[1].Substring(1);
               string[] a3 = a2.Split('<');

               return a3[0];
           }
           
           task.Wait();
               
           OnSuccess(task.Result);
       }

       void OnSuccess(string ip)
       {
           onSuccess?.Invoke(ip);
       }
       
       void OnError(string error)
       {
           onError?.Invoke(error);
       }
    }
}