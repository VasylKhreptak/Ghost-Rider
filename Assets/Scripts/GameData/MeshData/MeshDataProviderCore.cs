using System;
using UnityEngine;

public class MeshDataProviderCore : MonoEvent
{
	[Header("References")]
	[SerializeField] protected MeshFilter _meshFilter;

	[Header("Events")]
	[SerializeField] protected MonoEvent _updateEvent;
	
	#region MonoBehaviour

	private void OnValidate()
	{
		_meshFilter ??= GetComponent<MeshFilter>();
	}

	private void Awake()
	{
		UpdateData();
		
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
		_updateEvent.onMonoCall += UpdateData;
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
		_updateEvent.onMonoCall -= UpdateData;
	}
	
	protected virtual void UpdateData()
	{
		throw new NotImplementedException();
	}
}
