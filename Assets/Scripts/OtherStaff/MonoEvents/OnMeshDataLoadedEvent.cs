using UnityEngine;

public class OnMeshDataLoadedEvent: MonoEvent
{
	[Header("References")]
	[SerializeField] private MeshDataProviderCore _meshData;
	#region MonoBehaviour

	private void OnValidate()
	{
		_meshData ??= GetComponent<MeshDataProviderCore>();
	}

	private void OnEnable()
	{
		_meshData.onLoad += Invoke;
	}

	private void OnDisable()
	{
		_meshData.onLoad -= Invoke;
	}

	#endregion
	
}

