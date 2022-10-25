using UnityEngine;

public class OnMeshUpdateEvent : MonoEvent
{
	[Header("References")]
	[SerializeField] private MeshDeformation _meshDeformation;

	#region MonoBehaviour

	private void OnValidate()
	{
		_meshDeformation ??= GetComponent<MeshDeformation>();
	}

	private void Awake()
	{
		_meshDeformation.onMeshUpdate += Invoke;
	}

	private void OnDestroy()
	{
		_meshDeformation.onMeshUpdate -= Invoke;
	}

	#endregion
}
