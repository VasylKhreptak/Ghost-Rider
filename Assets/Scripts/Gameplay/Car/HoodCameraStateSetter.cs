using NaughtyAttributes;
using UnityEngine;

public class HoodCameraStateSetter : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private RCC_Camera _camera;
	[ShowIf(nameof(_resetLookDirection)), SerializeField] private MainCarSpawner _mainCarSpawner;

	[Header("Preferences")]
	[SerializeField] private bool _resetLookDirection = true;
	[SerializeField] private RCC_Camera.CameraMode _normalCameraMode = RCC_Camera.CameraMode.TPS;
		
	[Header("Events")]
	[SerializeField] private MonoEvent _enableHoodCameraEvent;
	[SerializeField] private MonoEvent _disableHoodCameraEvent;

	#region MonoBehaviour

	private void OnValidate()
	{
		_camera ??= GetComponent<RCC_Camera>();
	}

	private void Awake()
	{
		_camera.useHoodCameraMode = true;
	}

	private void OnEnable()
	{
		_enableHoodCameraEvent.onMonoCall += EnableHoodCamera;
		_disableHoodCameraEvent.onMonoCall += DisableHoodCamera;
	}

	private void OnDisable()
	{
		_enableHoodCameraEvent.onMonoCall -= EnableHoodCamera;
		_disableHoodCameraEvent.onMonoCall -= DisableHoodCamera;
	}

	#endregion

	private void EnableHoodCamera()
	{
		_camera.cameraMode = RCC_Camera.CameraMode.FPS;
		
		if (_resetLookDirection)
		{
			ResetLookDirection();
		}
	}

	private void DisableHoodCamera()
	{
		_camera.cameraMode = _normalCameraMode;
	}

	private void ResetLookDirection()
	{
		_camera.transform.forward = _mainCarSpawner.CurrentCar.Transform.forward;
	}
}
