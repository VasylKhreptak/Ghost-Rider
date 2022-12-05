using System;
using System.Collections;
using UnityEngine;

public class BackgroundMusicPlayer : MonoBehaviour
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

    private void Awake()
    {
        WebRequests.AudioClip.GetAsync(new Uri("https://firebasestorage.googleapis.com/v0/b/ghost-rider-1c1c4.appspot.com/o/Music%2F1997.mp3?alt=media&token=ba4cba12-4d6c-44b0-99b2-64743f8f54fb"),
            AudioType.MPEG, (clip) =>
            {
                Debug.Log(clip.length);
            });
    }

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
