using UnityEngine;
using UnityEngine.Audio;
using Zenject;

public class PlayableAudio : AudioClipHolder
{
    [Header("Audios")]
    [SerializeField] private Transform _transform;
    [SerializeField] private AudioClip[] _audioClips;

    [Header("Preferences")]
    [SerializeField] private AudioMixerGroup _output;
    [SerializeField] private bool _playOnTransformPosition;
    [SerializeField] private Transform _linkTo;
    [SerializeField] private Vector3 _position;
    [SerializeField, Range(0f, 1f)] private float _volume = 1f;
    [SerializeField, Range(0f, 1f)] private float _spatialBlend;
    [SerializeField, Range(0, 128)] private int _priority = 128;

    private AudioPooler _audioPooler;

    private int _id;

    [Inject]
    private void Construct(AudioPooler audioPooler)
    {
        _audioPooler = audioPooler;
    }

    #region MonoBehaviour

    private void OnValidate()
    {
        _transform ??= GetComponent<Transform>();
    }

    #endregion

    public override void Play()
    {
        _id = _audioPooler.PlaySound(_output, _audioClips.Random(), _playOnTransformPosition ? _transform.position : _position,
            _volume, _spatialBlend, linkTo:_linkTo, priority:_priority);
    }
    public override void Stop()
    {
        _audioPooler.StopSound(_id);
    }
}
