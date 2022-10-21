using UnityEngine;

public class OnMeshDeformEvent : MonoEvent
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
		_meshDeformation.onDeform += Invoke;
	}

	private void OnDisable()
	{
		_meshDeformation.onDeform -= Invoke;
	}

	#endregion
}
