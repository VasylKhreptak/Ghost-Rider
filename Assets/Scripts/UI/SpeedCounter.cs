using TMPro;
using UnityEngine;

public class SpeedCounter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text _tmpText;
    [SerializeField] private MainCarSpawner _mainCarSpawner;

    [Header("Preferences")]
    [SerializeField] private float _updateDelay;

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
        RCC_CarControllerV3 carController = _mainCarSpawner.CurrentCar.carController;
        
        _tmpText.text = ((int)carController.speed).ToString();
    }
}
