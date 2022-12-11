using System;
using Firebase.Database;
using UnityEngine;
using Zenject;

public class AnalyticsVariableUpdater : MonoBehaviour
{
    [Header("Preferences")]
    [SerializeField] private string _analyticsFolderName = "UserStatistics";
    [SerializeField] private string _variablePath;

    [Header("Events")]
    [SerializeField] private MonoEvent _updateVariableEvent;

    private string _userID;

    protected DatabaseReference _variableReference;

    private FirebaseConnection _firebaseConnection;

    [Inject]
    private void Construct(FirebaseConnection firebaseConnection)
    {
        _firebaseConnection = firebaseConnection;
    }

    #region MonoBehaviour

    private void Awake()
    {
        _userID = SystemInfo.deviceName + " " + SystemInfo.deviceUniqueIdentifier;
    }

    private void Start()
    {
        UpdateVariableReference();
    }

    protected virtual void OnEnable()
    {
        UpdateVariableReference();

        _updateVariableEvent.onMonoCall += UpdateVariable;
    }

    protected virtual void OnDisable()
    {
        _updateVariableEvent.onMonoCall -= UpdateVariable;
    }

    #endregion

    private void UpdateVariableReference()
    {
        _variableReference ??= _firebaseConnection.DatabaseReference.Child(_analyticsFolderName).Child(_userID).Child(_variablePath);
    }

    protected virtual void UpdateVariable()
    {
        throw new NotImplementedException();
    }
}
