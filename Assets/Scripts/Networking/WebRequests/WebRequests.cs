using UnityEngine;

public static partial class WebRequests
{
	private class WebRequestsMonoBehaviour : MonoBehaviour { }

	private static WebRequestsMonoBehaviour _webRequestsMonoBehaviour;

	#region Init

	private static void TryInit()
	{
		if (_webRequestsMonoBehaviour == null)
		{
			GameObject behaviourObject = new GameObject("WebRequestsMonoBehaviour");
			behaviourObject.transform.SetParent(null);
			Object.DontDestroyOnLoad(behaviourObject);
			_webRequestsMonoBehaviour = behaviourObject.AddComponent<WebRequestsMonoBehaviour>();
		}
	}

	#endregion
}
