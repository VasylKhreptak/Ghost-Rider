using System;
using System.Collections;
using UnityEngine.Networking;

public static partial class WebRequests
{
	public static class AudioClip
	{
		public static void GetAsync(Uri url, Action<UnityEngine.AudioClip> onSuccess, Action<string> onError = null)
		{
			TryInit();
			
			_webRequestsMonoBehaviour.StartCoroutine(GetAsyncRoutine(url, onSuccess, onError));
		}

		private static IEnumerator GetAsyncRoutine(Uri url, Action<UnityEngine.AudioClip> onSuccess, Action<string> onError)
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
					DownloadHandlerAudioClip downloadHandlerAudioClip = webRequest.downloadHandler as DownloadHandlerAudioClip;
					
					if (downloadHandlerAudioClip != null)
					{
						OnSuccess(downloadHandlerAudioClip.audioClip);
					}
					else
					{
						OnError(webRequest.error);
					}
				}

				void OnSuccess(UnityEngine.AudioClip text)
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
