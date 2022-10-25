using UnityEngine;

public class OnEventUpdateMeshDataProvider : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private MeshDataProviderCore _dataProvider;

	[Header("Events")]
	[SerializeField] private MonoEvent _monoEvent;

	#region MonoBehaviour

	private void OnValidate()
	{
		_dataProvider ??= GetComponent<MeshDataProviderCore>();
	}

	private void Awake()
	{
		_monoEvent.onMonoCall += _dataProvider.UpdateData;
	}

	private void OnDestroy()
	{
		_monoEvent.onMonoCall -= _dataProvider.UpdateData;
	}
	
	#endregion
}
