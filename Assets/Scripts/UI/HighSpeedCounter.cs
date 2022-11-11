using TMPro;
using UnityEngine;

public class HighSpeedCounter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text _tmpText;
    [SerializeField] private MainCarSpawner _mainCarSpawner;

    [Header("Preferences")]
    [SerializeField] private float _updateDelay;

    private SafeFloat _maxSpeed;

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
        RCC_CarControllerV3 carController = _mainCarSpawner.CurrentCar.CarController;
        
        if (carController.speed > _maxSpeed)
        {
            _maxSpeed = carController.speed;
        }

        _tmpText.text = ((int)_maxSpeed).ToString();
    }

    public void ResetValue()
    {
        _maxSpeed = 0;
        _tmpText.text = "0";
    }
}
