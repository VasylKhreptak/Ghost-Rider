using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CameraClippingSlider : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera _camera;
    [SerializeField] private Slider _slider;

    private SettingsProvider _settingsProvider;

    [Inject]
    private void Construct(SettingsProvider settingsProvider)
    {
        _settingsProvider = settingsProvider;
    }

    #region MonoBehaviour

    private void OnValidate()
    {
        _slider ??= GetComponent<Slider>();
        _camera = Camera.main;
    }

    private void Start()
    {
        _slider.value = _settingsProvider.settings.cameraClipping;
        SetClippingDistance(_settingsProvider.settings.cameraClipping);
    }

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(SetClippingDistance);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(SetClippingDistance);
    }

    #endregion

    private void SetClippingDistance(float value)
    {
        _camera.farClipPlane = value;

        _settingsProvider.settings.cameraClipping = value;
    }
}
