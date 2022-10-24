using System;
using UnityEngine;

public class MeshTrianglesProvider : MonoEvent
{
	[Header("References")]
	[SerializeField] protected MeshFilter _meshFilter;

	[Header("Events")]
	[SerializeField] protected MonoEvent _updateEvent;
	
	public MeshTriangle[] triangles;
	
	#region MonoBehaviour

	private void OnValidate()
	{
		_meshFilter ??= GetComponent<MeshFilter>();
	}

	private void Awake()
	{
		UpdateTriangles();
		
		TryAddListener();
	}

	private void OnDestroy()
	{
		TryRemoveListener();
	}

	#endregion

	private void TryAddListener()
	{
		if (_updateEvent != null)
		{
			AddListener();
		}
	}

	private void AddListener()
	{
		_updateEvent.onMonoCall += UpdateTriangles;
	}
	
	private void TryRemoveListener()
	{
		if (_updateEvent != null)
		{
			RemoveListener();
		}
	}

	private void RemoveListener()
	{
		_updateEvent.onMonoCall -= UpdateTriangles;
	}
	
	protected virtual void UpdateTriangles()
	{
		throw new NotImplementedException();
	}
}
