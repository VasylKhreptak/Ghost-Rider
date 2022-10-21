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
		foreach (var areaObject in _triggerArea.affectedObjects)
		{
			SetActiveObject(areaObject, enabled);
		}

		if (enabled == false)
		{
			_triggerArea.affectedObjects.Clear();
		}
	}

	protected virtual void SetActiveObject(Transform areaObject, bool enabled)
	{
		areaObject.gameObject.SetActive(enabled);
	}
}
