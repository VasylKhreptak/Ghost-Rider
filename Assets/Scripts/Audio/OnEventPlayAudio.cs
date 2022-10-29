using UnityEngine;

public class OnEventPlayAudio : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MonoEvent _monoEvent;
    [SerializeField] private SoundHolder _clipHolder;

    #region MonoBehaviour

    private void OnValidate()
    {
        _monoEvent ??= GetComponent<MonoEvent>();
        _clipHolder ??= GetComponent<PlayableAudio>();
    }

    private void Awake()
    {
        _monoEvent.onMonoCall += PlayAudio;
    }

    private void OnDestroy()
    {
        _monoEvent.onMonoCall -= PlayAudio;
    }

    #endregion

    private void PlayAudio()
    {
        _clipHolder.Play();
    }
}
