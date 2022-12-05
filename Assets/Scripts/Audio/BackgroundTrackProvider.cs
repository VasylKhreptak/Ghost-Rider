using System;
using System.Threading.Tasks;
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

    public Action onDataLoaded;

    [Inject]
    private void Construct(FirebaseConnection firebaseConnection)
    {
        _firebaseConnection = firebaseConnection;
    }

    #region MonoBehaviour

    private void Start()
    {
        _musicFolderReference = _firebaseConnection.StorageReference.Child(_musicFolderName);

        LoadMusicDataAsync(onDataLoaded, Debug.Log);
    }

    #endregion

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
        _musicFolderReference.Child(_trackNames.Random()).GetDownloadUrlAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted && task.Exception != null)
            {
                OnError(task.Exception.Message);
            }
            else if (task.IsCompletedSuccessfully)
            {
                OnGetDownloadUrl(task.Result);
            }

            void OnGetDownloadUrl(Uri uri)
            {
                WebRequests.AudioClip.GetAsync(uri, AudioType.MPEG, OnSuccess, OnError);
            }
        });

        void OnSuccess(AudioClip clip) => onSuccess?.Invoke(clip);

        void OnError(string error) => onError?.Invoke(error);
    }
}
