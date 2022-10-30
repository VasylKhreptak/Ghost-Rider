using System;
using UnityEngine;

public class MeshTriangleCenterLinker : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private Transform _transform;
	[SerializeField] private InstancedMeshTriangleCentersProvider _centersProvider;

	[Header("Preferences")]
	[SerializeField] private int _triangleIndex;
	[SerializeField] private bool _autoOffset = true;
	[SerializeField] private Vector3 _positionOffset;

	[Header("Events")]
	[SerializeField] private MonoEvent _restoreEvent;

	private Vector3 _startLocalPosition;
	
	#region MonoBehaviour

	private void OnValidate()
	{
		_transform ??= GetComponent<Transform>();
		_restoreEvent ??= GetComponent<MonoEvent>();
		
		if (_autoOffset && _transform != null)
		{
			_positionOffset = GetPositionOffset();
		}
	}
	private void Awake()
	{
		_startLocalPosition = _transform.localPosition;
		
		_restoreEvent.onMonoCall += Restore;
	}

	private void OnEnable()
	{
		_centersProvider.onUpdate += UpdatePosition;
	}

	private void OnDisable()
	{
		_centersProvider.onUpdate -= UpdatePosition;
	}

	private void OnDestroy()
	{
		_restoreEvent.onMonoCall -= Restore;
	}

	#endregion

	private void UpdatePosition()
	{
		_transform.position = _centersProvider.GetWorldCenter(_triangleIndex) + _positionOffset;
	}

	private Vector3 GetPositionOffset()
	{
		MeshFilter meshFilter = _centersProvider.GetComponent<MeshFilter>();
		Vector3 localTriangleCenter = meshFilter.sharedMesh.GetTriangleCenters()[_triangleIndex];
		Vector3 worldTriangleCenter = meshFilter.transform.TransformPoint(localTriangleCenter);

		return _transform.position - worldTriangleCenter;
	}

	private void Restore()
	{
		_transform.localPosition = _startLocalPosition;
	}
}
