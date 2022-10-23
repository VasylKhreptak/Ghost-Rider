using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class AudioPlayCooldown : SoundHolder
{
    [FormerlySerializedAs("_audioClipHolder")]
    [Header("References")]
    [SerializeField] private SoundHolder _soundHolder;

    [Header("Preferences")]
    [SerializeField] private float _cooldown;

    private Tween _waitTween;

    private bool _canPlay;

    #region Monobehaviour

    private void OnValidate()
    {
        _soundHolder ??= GetComponent<SoundHolder>();
    }

    private void OnDisable()
    {
        Stop();
    }

    #endregion

    public override void Play()
    {
        if (_canPlay)
        {
            _soundHolder.Play();
        }

        _canPlay = false;
        _waitTween = this.DOWait(_cooldown).OnComplete(() => { _canPlay = true; });
    }
    public override void Stop()
    {
        _waitTween.Kill();
    }
}
