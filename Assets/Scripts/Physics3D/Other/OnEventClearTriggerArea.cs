using UnityEngine;

public class OnEventClearTriggerArea : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private Transform _parent;
	[SerializeField] private TriggerArea[] _triggerAreas;

	[Header("Events")]
	[SerializeField] private MonoEvent _event;

	#region MonoBehaviour

	private void OnValidate()
	{
		_parent ??= transform.parent;
		_triggerAreas ??= _parent.GetComponentsInChildren<TriggerArea>();
	}

	private void Awake()
	{
		_event.onMonoCall += Clear;
	}

	private void OnDestroy()
	{
		_event.onMonoCall -= Clear;
	}

	#endregion
	
	private void Clear()
	{
		foreach (var triggerArea in _triggerAreas)
		{
			triggerArea.Clear();
		}
	}
}
