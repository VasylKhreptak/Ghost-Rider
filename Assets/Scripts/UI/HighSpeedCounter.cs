using TMPro;
using UnityEngine;

public class HighSpeedCounter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text _tmpText;
    [SerializeField] private RCC_CarControllerV3 _carController;

    [Header("Preferences")]
    [SerializeField] private float _updateDelay;

    private float _maxSpeed;

    private ConfigurableUpdate _configurableUpdate = new ConfigurableUpdate();
    
    #region MonoBehaviour

    private void OnValidate()
    {
        _tmpText ??= GetComponent<TMP_Text>();
    }

    private void Awake()
    {
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
        if (_carController.speed > _maxSpeed)
        {
            _maxSpeed = _carController.speed;
        }

        _tmpText.text = ((int)_maxSpeed).ToString();
    }
}
