using System;
using System.Collections;
using UnityEngine.Networking;

public static partial class WebRequests
{
	public static class Text
	{
		public static void GetAsync(Uri url, Action<string> onSuccess, Action<string> onError = null)
		{
			TryInit();
			
			_webRequestsMonoBehaviour.StartCoroutine(GetAsyncRoutine(url, onSuccess, onError));
		}

		private static IEnumerator GetAsyncRoutine(Uri url, Action<string> onSuccess, Action<string> onError)
		{
			using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
			{
				yield return webRequest.SendWebRequest();

				if (webRequest.result == UnityWebRequest.Result.ConnectionError ||
				    webRequest.result == UnityWebRequest.Result.ProtocolError ||
				    webRequest.result == UnityWebRequest.Result.DataProcessingError)
				{
					OnError(webRequest.error);
				}
				else if (webRequest.result == UnityWebRequest.Result.Success)
				{
					OnSuccess(webRequest.downloadHandler.text);
				}

				void OnSuccess(string text)
				{
					onSuccess?.Invoke(text);
				}

				void OnError(string error)
				{
					onError?.Invoke(error);
				}
			}
		}
	}
}
