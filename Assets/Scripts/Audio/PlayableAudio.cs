using UnityEngine;
using Zenject;

public class PlayableAudio : AudioClipHolder
{
    [Header("Audios")]
    [SerializeField] private AudioClip[] _audioClips;

    [Header("Preferences")]
    [SerializeField] private string _track;
    [SerializeField] private Vector3 _position;
    [SerializeField, Range(0f, 1f)] private float _volume = 1f;
    [SerializeField] private float _spatialBlend;
    [SerializeField] private int _priority = 128;

    private AudioPooler _audioPooler;

    private uint _id;

    [Inject]
    private void Construct(AudioPooler audioPooler)
    {
        _audioPooler = audioPooler;
    }
    
    public override void Play()
    {
        _id = _audioPooler.PlayOneShootSound(_track, _audioClips.Random(), _position, _volume, _spatialBlend, _priority);
    }
    public override void Stop()
    {
        _audioPooler.StopOneShootSound(_id);
    }
}
