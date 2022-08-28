using UnityEngine;

public class OnEventStopAudioDelay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioDelay _audioDelay;
    [SerializeField] private MonoEvent _monoEvent;

    #region MonoBehaviour

    private void OnValidate()
    {
        _audioDelay ??= GetComponent<AudioDelay>();
        _monoEvent ??= GetComponent<MonoEvent>();
    }

    private void Awake()
    {
        _monoEvent.onMonoCall += StopTimer;
    }

    private void OnDestroy()
    {
        _monoEvent.onMonoCall -= StopTimer;
    }

    #endregion

    private void StopTimer()
    {
        _audioDelay.StopTimer();
    }
}
