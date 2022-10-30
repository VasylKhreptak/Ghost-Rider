using System;
using UnityEngine;

public class NormalDeviationEvent : MonoEvent
{
	[Header("References")]
	[SerializeField] private InstancedMeshTriangleNormalsProvider _normalsProvider;

	[Header("Preferences")]
	[SerializeField] private int _normalIndex;
	[SerializeField] private float _maxAngle;

	[Header("Events")]
	[SerializeField] private MonoEvent _checkEvent;
	
	private Vector3 _startNormal;
	
	#region MonoBehaviour

	private void Awake()
	{
		_normalsProvider.onLoad += UpdateStartNormal;
	}

	private void OnEnable()
	{
		_checkEvent.onMonoCall += CheckDeviation;
	}

	private void OnDisable()
	{
		_checkEvent.onMonoCall -= CheckDeviation;
	}

	private void OnDestroy()
	{
		_normalsProvider.onLoad -= UpdateStartNormal;
	}

	#endregion

	private void UpdateStartNormal()
	{
		_startNormal = _normalsProvider.normals[_normalIndex];
	}
	
	private void CheckDeviation()
	{
		if (IsDeviated())
		{
			onMonoCall?.Invoke();
		}
	}

	private bool IsDeviated()
	{
		return Vector3.Angle(_startNormal, _normalsProvider.normals[_normalIndex]) > _maxAngle;
	}
}
