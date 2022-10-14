using UnityEngine;

public class CarHighPassEvent : MonoEvent
{
	[Header("References")]
	[SerializeField] private OnTriggerEnterEvent _carPassEvent;
	[SerializeField] private RCC_CarControllerV3 _carController;

	[Header("Preferences")]
	[SerializeField] private float _speedDifference;
	
	#region MonoBehaviour

	private void OnValidate()
	{
		_carPassEvent ??= GetComponent<OnTriggerEnterEvent>();
	}

	private void OnEnable()
	{
		_carPassEvent.onEnter += TryInvoke;
	}

	private void OnDisable()
	{
		_carPassEvent.onEnter -= TryInvoke;
	}

	#endregion

	private void TryInvoke(Collider collider)
	{
		if (collider.transform.parent.parent.TryGetComponent(out RCC_CarControllerV3 carController) 
		    && (carController.speed - _carController.speed) > _speedDifference)
		{
			onMonoCall?.Invoke();
		}
	}
}
