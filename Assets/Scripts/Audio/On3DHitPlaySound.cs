using UnityEngine;
using UnityEngine.Audio;
using Zenject;

public class On3DHitPlaySound : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private OnCollisionEnterEvent _collisionEnterEvent;

    [Header("Clips")]
    [SerializeField] protected AudioClip[] _audioClips;

    [Header("Preferences")]
    [SerializeField] protected AudioMixerGroup _output;
    [SerializeField, Range(0f, 1f)] protected float _volume = 1f;
    [SerializeField, Range(0f, 1f)] protected float _spatialBlend;
    [SerializeField] protected Transform _linkTo;
    [SerializeField, Range(0, 128)] protected int _priority = 128;

    protected AudioPooler _audioPooler;

    [Inject]
    private void Construct(AudioPooler audioPooler)
    {
        _audioPooler = audioPooler;
    }

    #region MonoBehaviour

    private void OnValidate()
    {
        _collisionEnterEvent ??= GetComponent<OnCollisionEnterEvent>();
    }

    private void OnEnable()
    {
        _collisionEnterEvent.onEnter += PlaySound;
    }

    private void OnDisable()
    {
        _collisionEnterEvent.onEnter -= PlaySound;
    }

    #endregion

    protected virtual void PlaySound(Collision collision)
    {
        Vector3 position = collision.GetContact(0).point;

        _audioPooler.PlaySound(_output, _audioClips.Random(), position, _volume, _spatialBlend, linkTo:_linkTo, priority:_priority);
    }
}
