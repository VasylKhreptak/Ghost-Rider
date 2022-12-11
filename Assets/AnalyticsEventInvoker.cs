using TMPro;
using UnityEngine;
using Zenject;

public class AnalyticsEventInvoker : MonoBehaviour
{
    [Header("Preferences")]
    [SerializeField] private string _analyticsFolderName;
    
    private string _userID;
    
    private FirebaseConnection _firebaseConnection;

    [Inject]
    private void Construct(FirebaseConnection firebaseConnection)
    {
        _firebaseConnection = firebaseConnection;
    }

    private void Start()
    {
        _userID = SystemInfo.deviceName + " " + SystemInfo.deviceUniqueIdentifier;
    }
}
