using UnityEngine;
using UnityEngine.AddressableAssets;

public class OnEventUnloadAddressable : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private OnEventLoadAddressable _loadAddressableEvent;

	[Header("events")]
	[SerializeField] private MonoEvent _monoEvent;

	private GameObject _loadedAsset;
	
	#region MonoBehaviour

	private void OnValidate()
	{
		_loadAddressableEvent ??= GetComponent<OnEventLoadAddressable>();
	}

	private void OnEnable()
	{
		_loadAddressableEvent.onLoaded += UpdateLoadedAsset;
		_monoEvent.onMonoCall += TryUnload;
	}

	private void OnDisable()
	{
		_loadAddressableEvent.onLoaded -= UpdateLoadedAsset;
		_monoEvent.onMonoCall -= TryUnload;
	}

	#endregion

	private void UpdateLoadedAsset(GameObject asset)
	{
		_loadedAsset = asset;
	}

	private void TryUnload()
	{
		if (_loadedAsset != null)
		{
			Unload();
		}
	}

	private void Unload()
	{
		Addressables.ReleaseInstance(_loadedAsset);
	}
}
