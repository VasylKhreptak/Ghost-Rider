using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class BackgroundMusicPlayer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioSource _audioSource;

    private Coroutine _playingCoroutine;

    private BackgroundTrackProvider _backgroundTrackProvider;
    private NetworkConnectionEvents _networkConnectionEvents;

    public Action<AudioClip> onTrackPlayed;

    [Inject]
    private void Construct(BackgroundTrackProvider backgroundTrackProvider, NetworkConnectionEvents networkConnectionEvents)
    {
        _backgroundTrackProvider = backgroundTrackProvider;
        _networkConnectionEvents = networkConnectionEvents;
    }

    #region MonoBehaviour

    private void OnValidate()
    {
        _audioSource ??= GetComponent<AudioSource>();
    }

    private void Awake()
    {
        _backgroundTrackProvider.onDataLoaded += OnTracksLoad;
    }

    private void OnDestroy()
    {
        _backgroundTrackProvider.onDataLoaded -= OnTracksLoad;
    }

    #endregion

    private void OnTracksLoad()
    {
        StopPlaying();

        StartPlaying();
    }

    public void StartPlaying()
    {
        if (_playingCoroutine == null)
        {
            _playingCoroutine = StartCoroutine(PlayingRoutine());
        }
    }

    public void StopPlaying()
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
            yield return new WaitUntil(() => _networkConnectionEvents.IsConnected);
            
            TryPlayRandomAudio();

            yield return new WaitWhile(() => _audioSource.clip == null);

            yield return new WaitForSeconds(_audioSource.clip.length);
        }
    }

    private void TryPlayRandomAudio()
    {
        TryDisposeClip();

        _backgroundTrackProvider.GetAudioClipAsync(OnSuccess, OnError);

        void OnSuccess(AudioClip audioClip)
        {
            _audioSource.clip = audioClip;

            _audioSource.Play();

            onTrackPlayed?.Invoke(audioClip);
        }

        void OnError(string error)
        {
            Debug.Log("Error: " + (error));
        }
    }

    private void TryDisposeClip()
    {
        AudioClip clip = _audioSource.clip;

        if (clip != null)
        {
            _audioSource.clip = null;
            clip.UnloadAudioData();
            AudioClip.Destroy(clip);
        }
    }
}
