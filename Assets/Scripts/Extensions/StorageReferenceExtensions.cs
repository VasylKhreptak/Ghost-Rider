using System;
using System.Threading.Tasks;
using Firebase.Extensions;
using Firebase.Storage;
using UnityEngine;

public static class StorageReferenceExtensions
{
    public static void GetChildrenReferencesAsync(this StorageReference storageReference, Action<StorageReference[]> onSuccess, Action<string> onError = null)
    {
        storageReference.GetChildrenNamesAsync(OnDownloadedNames, OnError);

        void OnDownloadedNames(string[] names)
        {
            StorageReference[] storageReferences = new StorageReference[names.Length];

            for (int i = 0; i < names.Length; i++)
            {
                storageReferences[i] = storageReference.Child(names[i]);
            }

            OnSuccess(storageReferences);
        }

        void OnSuccess(StorageReference[] storageReferences) => onSuccess?.Invoke(storageReferences);

        void OnError(string error) => onError?.Invoke(error);
    }

    public static void GetChildrenNamesAsync(this StorageReference storageReference, Action<string[]> onSuccess, Action<string> onError = null)
    {
        storageReference.Child("ChildrenNames.txt").GetDownloadUrlAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted && task.Exception != null)
            {
                OnError(task.Exception.Message);
            }
            else if (task.IsCompleted)
            {
                WebRequests.Text.GetAsync(task.Result, OnDownloadedText, OnError);
                
                void OnDownloadedText(string text)
                {
                    string[] names = text.Split(',');

                    OnSuccess(names);
                }
            }

            void OnSuccess(string[] names) => onSuccess?.Invoke(names);

            void OnError(string error) => onError?.Invoke(error);
        });
    }
}
