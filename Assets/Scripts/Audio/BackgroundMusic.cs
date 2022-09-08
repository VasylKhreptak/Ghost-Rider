using System.Collections;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioSource _audioSource;

    [Header("Preferences")]
    [SerializeField] private AudioClip[] _audioClips;

    private Coroutine _playingCoroutine;

    public float Volume
    {
        get => _audioSource.volume;
        set => _audioSource.volume = value;
    }

    #region MonoBehaviour

    private void OnValidate()
    {
        _audioSource ??= GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        StartPlaying();
    }

    private void OnDisable()
    {
        StopPlaying();
    }

    #endregion

    private void StartPlaying()
    {
        if (_playingCoroutine == null)
        {
            _playingCoroutine = StartCoroutine(PlayingRoutine());
        }
    }

    private void StopPlaying()
    {
        if (_playingCoroutine != null)
        {
            StopCoroutine(_playingCoroutine);

            _playingCoroutine = null;
        }
    }

    private IEnumerator PlayingRoutine()
    {
        while (true)
        {
            AudioClip clip = _audioClips.Random();

            _audioSource.clip = clip;

            _audioSource.Play();

            yield return new WaitForSeconds(clip.length);
        }
    }
}
