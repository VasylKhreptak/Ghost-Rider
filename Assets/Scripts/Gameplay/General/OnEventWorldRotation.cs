using System;
using UnityEngine;

public class OnEventWorldRotation : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private Transform _transform;

	[Header("Preferences")]
	[SerializeField] private Vector3 _rotation;

	[Header("Events")]
	[SerializeField] private MonoEvent _event;

	#region MonoBehaviour

	private void OnValidate()
	{
		_transform ??= GetComponent<Transform>();
		_event ??= GetComponent<MonoEvent>();
	}

	private void Awake()
	{
		_event.onMonoCall += SetRotation;
	}

	private void OnDestroy()
	{
		_event.onMonoCall -= SetRotation;
	}

	#endregion

	private void SetRotation()
	{
		_transform.rotation = Quaternion.Euler(_rotation);
	}
}
