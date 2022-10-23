using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;
using Zenject;

public class PlayableAudio : SoundHolder
{
    [Header("Audios")]
    [SerializeField] private Transform _transform;
    [SerializeField] private AudioClip[] _audioClips;

    [Header("Preferences")]
    [SerializeField] private AudioMixerGroup _output;
    [SerializeField] private bool _playOnTransformPosition;
    [SerializeField] private Transform _linkTo;
    [SerializeField] [FormerlySerializedAs("_position")]
    private Vector3 _startPosition;
    [SerializeField, Range(0f, 1f)] private float _volume = 1f;
    [SerializeField, Range(0f, 1f)] private float _spatialBlend;
    [SerializeField, Range(0, 128)] private int _priority = 128;
    [SerializeField] private bool _loop;

    private AudioPooler _audioPooler;

    private int _currentAudioID;
    private AudioPoolItem _currentAudioItem;

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
        _currentAudioID = _audioPooler.PlaySound(_output, _audioClips.Random(), _playOnTransformPosition ? _transform.position : _startPosition,
            _volume, _spatialBlend, _loop, _linkTo, _priority);

        _currentAudioItem = _audioPooler.GetAudioPoolItem(_currentAudioID);
    }
    public override void Stop()
    {
        _audioPooler.StopSound(_currentAudioID);

        _currentAudioItem = null;
    }

    public bool TrySetAudioPosition(Vector3 position)
    {
        if (IsPlaying())
        {
            SetAudioPosition(ref position);

            return true;
        }

        return false;
    }

    private bool IsPlaying()
    {
        if (_currentAudioItem == null) return false;
        
        return _currentAudioItem.audioSource.isPlaying;
    }

    private void SetAudioPosition(ref Vector3 position)
    {
        _currentAudioItem.transform.position = position;
    }

    public AudioPoolItem GetAudioItem()
    {
        return _currentAudioItem;
    }
}
