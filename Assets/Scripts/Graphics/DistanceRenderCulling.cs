using UnityEngine;

public class DistanceRenderCulling : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private DistanceRenderingEvents _distanceRenderingEvents;

	[Header("Renderers List")]
	[SerializeField] private Renderer[] _renderers;
	
	#region MonoBehaviour

	private void OnValidate()
	{
		_distanceRenderingEvents ??= GetComponent<DistanceRenderingEvents>();

		_renderers ??= transform.GetComponentsInChildren<Renderer>(true);
	}
	
	private void OnEnable()
	{
		_distanceRenderingEvents.onStartRendering += EnableRendering;
		_distanceRenderingEvents.onStopRendering += DisableRendering;
	}

	private void OnDisable()
	{
		_distanceRenderingEvents.onStartRendering -= EnableRendering;
		_distanceRenderingEvents.onStopRendering -= DisableRendering;
	}

	#endregion

	private void EnableRendering() => SetRenderersState(true);

	private void DisableRendering() => SetRenderersState(false);

	private void SetRenderersState(bool state)
	{
		foreach (var renderer in _renderers)
		{
			renderer.enabled = state;
		}
	}
}
