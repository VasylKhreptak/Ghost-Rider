using UnityEngine;

public class OnAudioPlayEvent : MonoEvent
{
	[Header("References")]
	[SerializeField] private PlayableAudio _playableAudio;

	#region MonoBehaviouor

	private void OnValidate()
	{
		_playableAudio ??= GetComponent<PlayableAudio>();
	}

	private void OnEnable()
	{
		_playableAudio.onPlay += Invoke;
	}

	private void OnDisable()
	{
		_playableAudio.onPlay -= Invoke;
	}

	#endregion
}
