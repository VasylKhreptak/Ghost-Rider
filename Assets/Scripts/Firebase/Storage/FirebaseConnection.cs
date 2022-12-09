using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using Firebase.Storage;
using UnityEngine;

public class FirebaseConnection : MonoBehaviour
{
	[Header("Preferences")]
	[SerializeField] private string _storageURL = "gs://ghost-rider-1c1c4.appspot.com/";

	private FirebaseStorage _storage;
	private StorageReference _storageReference;
	private FirebaseApp _firebaseApp;

	public FirebaseStorage Storage => _storage;
	public StorageReference StorageReference => _storageReference;
	public FirebaseApp FirebaseApp => _firebaseApp;

	#region MonoBehaviour

	private void Awake()
	{
		InitFirebaseStorage();

		IntiFirebaseAnalytics();
	}

	#endregion

	private void InitFirebaseStorage()
	{
		_storage = FirebaseStorage.DefaultInstance;
		_storageReference = _storage.GetReferenceFromUrl(_storageURL);
	}

	private void IntiFirebaseAnalytics()
	{
		FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
		{
			FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
			
			_firebaseApp = FirebaseApp.DefaultInstance;
		});
	}
}