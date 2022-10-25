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

	private void OnEnable()
	{
		_meshDeformation.onMeshUpdate += Invoke;
	}

	private void OnDisable()
	{
		_meshDeformation.onMeshUpdate -= Invoke;
	}

	#endregion
}
