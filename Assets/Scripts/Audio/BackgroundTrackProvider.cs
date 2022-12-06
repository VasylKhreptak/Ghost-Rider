using System;
using System.Collections;
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

    private IEnumerator Start()
    {
        _musicFolderReference = _firebaseConnection.StorageReference.Child(_musicFolderName);

        LoadMusicDataAsync(onDataLoaded, Debug.Log);

        yield return new WaitForSeconds(4f);
        
        // WebRequests.AudioClip.GetAsync(new Uri("https://firebasestorage.googleapis.com/v0/b/ghost-rider-1c1c4.appspot.com/o/ukrayinske-nebo-zaxishhayut-bogi-podolyak-pro-novii-udar-rosiyi.mp3?alt=media&token=18929ebf-fa54-4b6c-93b5-fcccc8b05032"), AudioType.MPEG, (clip) =>
        // {
        //     Debug.Log(clip.length);
        // });
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
