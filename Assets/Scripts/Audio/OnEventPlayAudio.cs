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

    private void OnEnable()
    {
        _monoEvent.onMonoCall += PlayAudio;
    }

    private void OnDisable()
    {
        _monoEvent.onMonoCall -= PlayAudio;
    }

    #endregion

    private void PlayAudio()
    {
        _clipHolder.Play();
    }
}
