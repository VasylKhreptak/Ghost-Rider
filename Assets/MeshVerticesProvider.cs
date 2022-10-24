using System;
using UnityEngine;

public class MeshVerticesProvider : MonoEvent
{
	[Header("References")]
	[SerializeField] protected MeshFilter _meshFilter;

	[Header("Events")]
	[SerializeField] protected MonoEvent _updateEvent;
	
	public Vector3[] vertices;
	
	#region MonoBehaviour

	private void OnValidate()
	{
		_meshFilter ??= GetComponent<MeshFilter>();
	}

	private void Awake()
	{
		UpdateVertices();
		
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
		_updateEvent.onMonoCall += UpdateVertices;
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
		_updateEvent.onMonoCall -= UpdateVertices;
	}
	
	protected virtual void UpdateVertices()
	{
		throw new NotImplementedException();
	}
}
