using UnityEngine;

public class PlayableAudioGroupClipsSetter : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private PlayableAudio _playableAudio;

	[Header("Preferences")]
	[SerializeField] private AudioGroup[] _audioGroups;

	[Header("Event")]
	[SerializeField] private MonoEvent _event;

	#region MonoBehaviour

	private void OnValidate()
	{
		_playableAudio ??= GetComponent<PlayableAudio>();
	}

	private void Awake()
	{
		_event.onMonoCall += SetGroup;
	}

	private void OnDestroy()
	{
		_event.onMonoCall -= SetGroup;
	}

	#endregion

	private void SetGroup()
	{
		_playableAudio.SetAudioClips(_audioGroups.Random().sounds);
	}
	
	[System.Serializable]
	private class AudioGroup
	{
		public AudioClip[] sounds;
	}
}
