using UnityEngine;

public class AdaptedAudioSourceForVolume : FloatAdapter
{
    [Header("References")]
    [SerializeField] private AudioSource _audioSource;


    #region MonoBehaviour

    private void OnValidate()
    {
        _audioSource ??= GetComponent<AudioSource>();
    }

    #endregion
    
    public override float value
    {
        get => _audioSource.volume;
        set => _audioSource.volume = value;
    }
}
