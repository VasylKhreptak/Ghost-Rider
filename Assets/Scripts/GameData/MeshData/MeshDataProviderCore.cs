using System;
using UnityEngine;

public class MeshDataProviderCore : MonoEvent
{
	[Header("References")]
	[SerializeField] protected MeshFilter _meshFilter;

	#region MonoBehaviour

	private void OnValidate()
	{
		_meshFilter ??= GetComponent<MeshFilter>();
	}

	private void Awake()
	{
		UpdateData();
	}
	#endregion

	public virtual void UpdateData()
	{
		throw new NotImplementedException();
	}
}
