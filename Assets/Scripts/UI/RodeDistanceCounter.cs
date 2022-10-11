using TMPro;
using UnityEngine;

public class RodeDistanceCounter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _target;
    [SerializeField] private TMP_Text _tmpText;

    [Header("Preferences")]
    [SerializeField] private float _updateDelay;
    [SerializeField] private float _amplifier;
    [SerializeField] private string _format;
    
    private SafeFloat _rodeDistance;

    private Vector3 _previousPosition;
    
    private ConfigurableUpdate _configurableUpdate = new ConfigurableUpdate();

    #region Monobehaviour

    private void OnValidate()
    {
        _tmpText ??= GetComponent<TMP_Text>();
    }

    private void Awake()
    {
        _previousPosition = _target.position;
        
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
        float distance = Vector3.Distance(_target.position, _previousPosition);

        _rodeDistance += distance;

        _tmpText.text = (_rodeDistance * _amplifier).ToString(_format);
        
        _previousPosition = _target.position;
    }

    private void ResetDistance()
    {
        _rodeDistance = 0;
    }
}
