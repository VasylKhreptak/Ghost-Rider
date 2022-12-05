using System;
using Firebase.Storage;
using UnityEngine;

public class FirebaseConnection : MonoBehaviour
{
	[Header("Preferences")]
	[SerializeField] private string _storageURL = "gs://ghost-rider-1c1c4.appspot.com/";
	
	private FirebaseStorage _storage;
	private StorageReference _storageReference;
	
	public FirebaseStorage Storage => _storage;
	public StorageReference StorageReference => _storageReference;

	#region MonoBehaviour

	private void Awake()
	{
		_storage = FirebaseStorage.DefaultInstance;
		_storageReference = _storage.GetReferenceFromUrl(_storageURL);
	}

	#endregion
}
