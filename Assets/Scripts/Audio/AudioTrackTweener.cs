using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;

public class AudioTrackTweener : AnimationCore
{
    [Header("Preferences")]
    [SerializeField] private AudioMixerGroup _output;
    [SerializeField, Range(-80f, 20f)] private float _startVolume;
    [SerializeField, Range(-80f, 20f)] private float _targetVolume;
    [SerializeField] private float _duration;
    [SerializeField] private AnimationCurve _animationCurve;

    private Tween _tween;

    private AudioMixer _audioMixer;
    
    private float _volume
    {
        get
        {
            _audioMixer.GetFloat(_output.name, out float volume);
            return volume;
        }
        set => _audioMixer.SetFloat(_output.name, value);
    }

    #region MonoBehaviour

    private void Awake()
    {
        _audioMixer = _output.audioMixer;
    }
    private void OnDestroy()
    {
        Kill();
    }

    #endregion

    public override void Animate(bool state)
    {
        _volume = state ? _startVolume : _targetVolume;

        DOTween
            .To(() => _volume, x => _volume = x, state ? _targetVolume : _startVolume, _duration)
            .SetEase(_animationCurve);
    }

    public override void Pause()
    {
        _tween.Pause();
    }
    public override void Play()
    {
        _tween.Play();
    }
    public override void Kill()
    {
        _tween.Kill();
    }
}
