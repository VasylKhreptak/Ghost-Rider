using Firebase;
using Firebase.Analytics;
using Firebase.Database;
using Firebase.Storage;
using UnityEngine;

public class FirebaseConnection : MonoBehaviour
{
    [Header("Preferences")]
    [SerializeField] private string _storageURL = "gs://ghost-rider-1c1c4.appspot.com/";
    [SerializeField] private string _databaseURL = "https://ghost-rider-1c1c4-default-rtdb.firebaseio.com/";

    private FirebaseStorage _storage;
    private StorageReference _storageReference;
    private FirebaseApp _firebaseApp;
    private FirebaseDatabase _firebaseDatabase;
    private DatabaseReference _databaseReference;

    public FirebaseStorage Storage => _storage;
    public StorageReference StorageReference => _storageReference;
    public FirebaseApp FirebaseApp => _firebaseApp;
    public DatabaseReference DatabaseReference => _databaseReference;

    #region MonoBehaviour

    private void Awake()
    {
        Init();
    }

    #endregion

    private void Init()
    {
        InitStorage();

        IntiAnalytics();

        InitDatabase();
    }
    
    private void InitStorage()
    {
        _storage = FirebaseStorage.DefaultInstance;
        _storageReference = _storage.GetReferenceFromUrl(_storageURL);
    }

    private void IntiAnalytics()
    {
        FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
        _firebaseApp = FirebaseApp.DefaultInstance;
    }

    private void InitDatabase()
    {
        _firebaseDatabase = FirebaseDatabase.GetInstance(_databaseURL);
        _databaseReference = _firebaseDatabase.RootReference;
    }
}
