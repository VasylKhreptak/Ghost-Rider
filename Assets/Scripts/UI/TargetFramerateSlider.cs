using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TargetFramerateSlider : UIUpdatableItem
{
    [Header("References")]
    [SerializeField] private Slider _slider;
    [SerializeField] private GameFramerate _gameFramerate;

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
    }

    private void Start()
    {
        UpdateValue();
    }

    public override void UpdateValue()
    {
        _slider.maxValue = Screen.currentResolution.refreshRate;

        _slider.value = _settingsProvider.settings.targetFramerate;

        _gameFramerate.Set(_settingsProvider.settings.maxFramerateEnabled ? int.MaxValue : _settingsProvider.settings.targetFramerate);
    }

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(SetFramerate);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(SetFramerate);
    }

    #endregion

    private void SetFramerate(float framerate)
    {
        _gameFramerate.Set((int)framerate);
        _settingsProvider.settings.targetFramerate = (int)framerate;
    }
}
