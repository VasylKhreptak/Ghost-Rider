using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using Zenject;

public class DistanceBasedUpdateEvent : MonoEvent
{
	[Header("References")]
	[SerializeField] private Transform _transform;
	[HideIf(nameof(_useMainCameraAsTarget)), SerializeField] private Transform _target;

	[Header("Preferences")]
	[SerializeField] private bool _affectedByPause = true;
	[SerializeField] private bool _useMainCameraAsTarget;
	[SerializeField] private float _minDistance;
	[SerializeField] private float _maxDistance;
	[SerializeField] private float _minDelay;
	[SerializeField] private float _maxDelay;
	[SerializeField] private AnimationCurve _delayCurve;

	private Coroutine _updateCoroutine;

	private PauseManager _pauseManager;

	[Inject]
	private void Construct(PauseManager pauseManager)
	{
		_pauseManager = pauseManager;
	}
	
	#region MonoBehaviour

	private void OnValidate()
	{
		_transform ??= GetComponent<Transform>();

		if (_useMainCameraAsTarget)
		{
			_target = Camera.main.transform;
		}
	}

	private void Awake()
	{
		if (_target == null && _useMainCameraAsTarget)
		{
			_target = Camera.main.transform;
		}
	}

	private void OnEnable()
	{
		StartUpdating();
	}

	private void OnDisable()
	{
		StopUpdating();
	}

	#endregion

	private void StartUpdating()
	{
		if (_updateCoroutine == null)
		{
			_updateCoroutine = StartCoroutine(UpdateRoutine());
		}
	}

	private void StopUpdating()
	{
		if (_updateCoroutine != null)
		{
			StopCoroutine(_updateCoroutine);

			_updateCoroutine = null;
		}
	}

	private IEnumerator UpdateRoutine()
	{
		while (true)
		{
			if (_affectedByPause == false)
			{
				Invoke();
			}
			else if(_pauseManager.isPaused == false)
			{
				Invoke();
			}
			
			yield return new WaitForSeconds(GetDelay());
		}	
	}

	private float GetDelay()
	{
		float distance = Vector3.Distance(_transform.position, _target.position);

		return _delayCurve.Evaluate(_minDistance, _maxDistance, distance, _minDelay, _maxDelay);
	}
}
