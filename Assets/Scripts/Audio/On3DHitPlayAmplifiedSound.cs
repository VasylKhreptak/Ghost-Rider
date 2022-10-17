using UnityEngine;

public sealed class On3DHitPlayAmplifiedSound : On3DHitPlaySound
{
    [SerializeField] private float _volumeAmplifier =  1f;
    [SerializeField] private float _minVolume;
   
    protected override void PlaySound(Collision collision)
    {
        Vector3 position = collision.GetContact(0).point;

        float volume = _volume * _volumeAmplifier * collision.impulse.magnitude;

        volume = Mathf.Clamp(volume, _minVolume, _volume);
        
        _audioPooler.PlaySound(_output, _audioClips.Random(), position, volume, _spatialBlend, _linkTo, _priority);
    }
}
