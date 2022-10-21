using UnityEngine;

public class ScratchAudio : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ScratchEvents _scratchEvents;
    [SerializeField] private PlayableAudio _audio;

    #region MonoBehaviour

    private void OnValidate()
    {
        _scratchEvents ??= GetComponent<ScratchEvents>();
        _audio ??= GetComponent<PlayableAudio>();
    }

    private void OnEnable()
    {
        _scratchEvents.onStartScratching += PlayAudio;
        _scratchEvents.onScratchUpdate += UpdateAudioPosition;
        _scratchEvents.onStopScratching += StopAudio;
    }

    private void OnDisable()
    {
        _scratchEvents.onStartScratching -= PlayAudio;
        _scratchEvents.onScratchUpdate -= UpdateAudioPosition;
        _scratchEvents.onStopScratching -= StopAudio;
    }

    #endregion

    private void PlayAudio(Vector3 position)
    {
        _audio.Play();
    }

    private void UpdateAudioPosition(Vector3 position)
    {
        _audio.TrySetAudioPosition(position);
    }

    private void StopAudio()
    {
        _audio.Stop();
    }
}
