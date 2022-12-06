using System;
using Firebase.Extensions;
using Firebase.Storage;
using UnityEngine;
using Zenject;
using Uri = System.Uri;

public class BackgroundTrackProvider : MonoBehaviour
{
    [Header("Preferences")]
    [SerializeField] private string _musicFolderName = "Music";

    private StorageReference _musicFolderReference;

    private string[] _trackNames;

    private FirebaseConnection _firebaseConnection;

    private NetworkConnectionEvents _networkConnectionEvents;

    private bool _wasLoaded;

    public Action onDataLoaded;

    [Inject]
    private void Construct(FirebaseConnection firebaseConnection, NetworkConnectionEvents networkConnectionEvents)
    {
        _firebaseConnection = firebaseConnection;
        _networkConnectionEvents = networkConnectionEvents;
    }

    #region MonoBehaviour

    private void Start()
    {
        _musicFolderReference = _firebaseConnection.StorageReference.Child(_musicFolderName);

        NetworkConnection.CheckAsync((isConnected) =>
        {
            if (isConnected)
            {
                TryLoadMusicDataAsync();
            }
        });

        _networkConnectionEvents.onConnected += TryLoadMusicDataAsync;
    }

    private void OnDestroy()
    {
        _networkConnectionEvents.onConnected -= TryLoadMusicDataAsync;
    }

    #endregion

    private void TryLoadMusicDataAsync() => TryLoadMusicDataAsync(onDataLoaded);

    private void TryLoadMusicDataAsync(Action onSuccess, Action<string> onError = null)
    {
        if (_wasLoaded == false)
        {
            LoadMusicDataAsync(OnLoaded, onError);

            void OnLoaded()
            {
                _wasLoaded = true;

                onSuccess?.Invoke();
            }
        }
    }

    private void LoadMusicDataAsync(Action onSuccess, Action<string> onError = null)
    {
        _musicFolderReference.GetChildrenNamesAsync(OnSuccess, OnError);

        void OnSuccess(string[] names)
        {
            _trackNames = names;

            onSuccess?.Invoke();
        }

        void OnError(string error) => onError?.Invoke(error);
    }

    public void GetAudioClipAsync(Action<AudioClip> onSuccess, Action<string> onError = null)
    {
        string trackName = _trackNames.Random();

        _musicFolderReference.Child(trackName).GetDownloadUrlAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted && task.Exception != null)
            {
                OnError(task.Exception.Message);
            }
            else if (task.IsCompletedSuccessfully)
            {
                OnDownloadedUrl(task.Result);
            }

            void OnDownloadedUrl(Uri uri)
            {
                WebRequests.AudioClip.GetAsync(uri, AudioType.MPEG, OnSuccess, OnError);
            }
        });

        void OnSuccess(AudioClip clip)
        {
            clip.name = trackName;

            onSuccess?.Invoke(clip);
        }

        void OnError(string error) => onError?.Invoke(error);
    }
}
