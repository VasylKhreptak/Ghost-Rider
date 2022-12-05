using System.Collections;
using UnityEngine;
using Zenject;

public class BackgroundMusicPlayer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioSource _audioSource;

    private Coroutine _playingCoroutine;

    private BackgroundTrackProvider _backgroundTrackProvider;

    [Inject]
    private void Construct(BackgroundTrackProvider backgroundTrackProvider)
    {
        _backgroundTrackProvider = backgroundTrackProvider;
    }

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
