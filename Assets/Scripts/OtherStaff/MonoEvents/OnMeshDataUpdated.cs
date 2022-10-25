using System;
using UnityEngine;

public class OnMeshDataUpdated : MonoEvent
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
		_meshData.onUpdate += Invoke;
	}

	private void OnDisable()
	{
		_meshData.onUpdate -= Invoke;
	}

	#endregion
}
