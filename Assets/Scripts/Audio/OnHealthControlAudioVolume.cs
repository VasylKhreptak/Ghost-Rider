using System;
using UnityEngine;

public class OnHealthControlAudioVolume : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DamageableObject _damageableObject;
    [SerializeField] private PlayableAudio _playableAudio;

    [Header("Preferences")]
    [SerializeField] private float _minHealth;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _minVolume;
    [SerializeField] private float _maxVolume;
    [SerializeField] private AnimationCurve _volumeCurve;

    [Header("Events")]
    [SerializeField] private MonoEvent _addListenersEvent;
    [SerializeField] private MonoEvent _removeListenersEvent;

    #region MonoBejaviour

    private void OnValidate()
    {
        _damageableObject ??= GetComponent<DamageableObject>();
        _playableAudio ??= GetComponent<PlayableAudio>();
    }

    private void Awake()
    {
        _addListenersEvent.onMonoCall += AddListeners;
        _removeListenersEvent.onMonoCall += RemoveListeners;
    }

    private void OnDestroy()
    {
        _addListenersEvent.onMonoCall -= AddListeners;
        _removeListenersEvent.onMonoCall -= RemoveListeners;
    }

    #endregion

    private void AddListeners()
    {
        _damageableObject.onSetHealth += OnSetHealth;
    }

    private void RemoveListeners()
    {
        _damageableObject.onSetHealth -= OnSetHealth;
    }
    
    private void OnSetHealth(float health)
    {
        if (HasAppropriateHealth(ref health))
        {
            SetVolume(GetVolume(ref health));
        }
        else
        {
            StopAudio();
        }
    }

    private bool HasAppropriateHealth(ref float health)
    {
        return health < _maxHealth;
    }

    private void SetVolume(float volume)
    {
        _playableAudio.TrySetVolume(volume);
    }

    private float GetVolume(ref float health)
    {
        return _volumeCurve.Evaluate(_minHealth, _maxHealth, health, _maxVolume, _minVolume);
    }

    private void StopAudio()
    {
        _playableAudio.Stop();
    }
}
