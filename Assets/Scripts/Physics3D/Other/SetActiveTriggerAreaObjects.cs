using UnityEngine;

public class SetActiveTriggerAreaObjects : MonoBehaviour
{
	[Header("References")]
	[SerializeField] protected TriggerArea _triggerArea;

	#region MonoBehaviour

	private void OnValidate()
	{
		_triggerArea ??= GetComponent<TriggerArea>();
	}

	#endregion

	public void SetActiveAreaObjects(bool enabled)
	{
		foreach (var collider in _triggerArea.affectedObjects)
		{
			SetActiveObject(collider, enabled);
		}

		if (enabled == false)
		{
			_triggerArea.affectedObjects.Clear();
		}
	}

	protected virtual void SetActiveObject(Collider collider, bool enabled)
	{
		collider.gameObject.SetActive(enabled);
	}
}
