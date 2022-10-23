using UnityEngine;

public class OnEventStopAudio : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private SoundHolder _audio;

	[Header("Event")]
	[SerializeField] private MonoEvent _event;
	
	#region MonoBehaviour

	private void OnValidate()
	{
		_audio ??= GetComponent<SoundHolder>();
	}

	private void Awake()
	{
		_event.onMonoCall += _audio.Stop;
	}

	private void OnDestroy()
	{
		_event.onMonoCall -= _audio.Stop;
	}

	#endregion
}
