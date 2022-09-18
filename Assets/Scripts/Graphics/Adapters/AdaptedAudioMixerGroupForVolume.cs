using UnityEngine;
using UnityEngine.Audio;

public class AdaptedAudioMixerGroupForVolume : FloatAdapter
{
    [Header("References")]
    [SerializeField] private AudioMixerGroup _audioMixerGroup;

    public override float value
    {
        get
        {
            _audioMixerGroup.audioMixer.GetFloat(_audioMixerGroup.name, out float value);
            return value;
        }
        set => _audioMixerGroup.audioMixer.SetFloat(_audioMixerGroup.name, value);
    }
}
