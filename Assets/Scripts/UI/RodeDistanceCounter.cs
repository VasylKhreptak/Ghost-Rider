using TMPro;
using UnityEngine;

public class RodeDistanceCounter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MainCarSpawner _mainCarSpawner;
    [SerializeField] private TMP_Text _tmpText;

    [Header("Preferences")]
    [SerializeField] private float _updateDelay;
    [SerializeField] private float _amplifier;
    [SerializeField] private string _format;
    
    private SafeFloat _rodeDistance;

    private Vector3 _previousPosition;
    private Vector3 _startPosition;
    
    private ConfigurableUpdate _configurableUpdate = new ConfigurableUpdate();

    #region Monobehaviour

    private void OnValidate()
    {
        _tmpText ??= GetComponent<TMP_Text>();
    }

    private void Awake()
    {
        Transform target = _mainCarSpawner.CurrentCar.transform;
        
        _previousPosition = target.position;
        _startPosition = target.position;
        _configurableUpdate.Init(this, _updateDelay, UpdateValue);
    }

    private void OnEnable()
    {
        _configurableUpdate.StartUpdating();
    }

    private void OnDisable()
    {
        _configurableUpdate.StopUpdating();
    }

    #endregion
    
    private void UpdateValue()
    {
        Transform target = _mainCarSpawner.CurrentCar.transform;

        float distance = Vector3.Distance(target.position, _previousPosition);

        _rodeDistance += distance;

        _tmpText.text = (_rodeDistance * _amplifier).ToString(_format);
        
        _previousPosition = target.position;
    }

    public void ResetDistance()
    {
        _rodeDistance = 0;
        _tmpText.text = "0";
        _previousPosition = _startPosition;
    }
}
