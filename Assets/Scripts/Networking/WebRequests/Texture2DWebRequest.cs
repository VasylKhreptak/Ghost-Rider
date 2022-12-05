using System;
using System.Collections;
using UnityEngine.Networking;

public static partial class WebRequests
{
	public static class Texture2D
	{
		public static void GetAsync(Uri url, Action<UnityEngine.Texture2D> onSuccess, Action<string> onError = null)
		{
			TryInit();
			
			_webRequestsMonoBehaviour.StartCoroutine(GetAsyncRoutine(url, onSuccess, onError));
		}

		private static IEnumerator GetAsyncRoutine(Uri url, Action<UnityEngine.Texture2D> onSuccess, Action<string> onError)
		{
			using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url))
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
					DownloadHandlerTexture downloadHandlerTexture = webRequest.downloadHandler as DownloadHandlerTexture;
					
					if (downloadHandlerTexture != null)
					{
						OnSuccess(downloadHandlerTexture.texture);
					}
					else
					{
						OnError("Something went wrong!");
					}
				}

				void OnSuccess(UnityEngine.Texture2D text)
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
