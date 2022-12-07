using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class OnEventLoadAddressable : MonoBehaviour
{
	[Header("Preferences")]
	[SerializeField] private AssetReference _asset;
	[SerializeField] private Vector3 _position;
	[SerializeField] private Vector3 _rotation;

	[Header("Events")]
	[SerializeField] private MonoEvent _monoEvent;

	public Action<GameObject> onLoaded; 
	
	#region MonoBehaviour

	private void OnValidate()
	{
		_monoEvent ??= GetComponent<MonoEvent>();
	}

	private void OnEnable()
	{
		_monoEvent.onMonoCall += Load;
	}

	private void OnDisable()
	{
		_monoEvent.onMonoCall -= Load;
	}
	
	#endregion

	private void Load()
	{
		AsyncOperationHandle<GameObject> asyncOperationHandle =_asset.InstantiateAsync(_position, Quaternion.Euler(_rotation));

		asyncOperationHandle.Completed += (asyncOperation) => onLoaded?.Invoke(asyncOperation.Result);
	}
}
