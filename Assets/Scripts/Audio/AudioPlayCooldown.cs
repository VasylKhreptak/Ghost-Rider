using DG.Tweening;
using UnityEngine;

public class AudioPlayCooldown : AudioClipHolder
{
    [Header("References")]
    [SerializeField] private AudioClipHolder _audioClipHolder;

    [Header("Preferences")]
    [SerializeField] private float _cooldown;

    private Tween _waitTween;

    private bool _canPlay;

    #region Monobehaviour

    private void OnValidate()
    {
        _audioClipHolder ??= GetComponent<AudioClipHolder>();
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
            _audioClipHolder.Play();
        }

        _canPlay = false;
        _waitTween = this.DOWait(_cooldown).OnComplete(() => { _canPlay = true; });
    }
    public override void Stop()
    {
        _waitTween.Kill();
    }
}
