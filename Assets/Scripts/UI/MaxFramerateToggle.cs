using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MaxFramerateToggle : UIUpdatableItem
{
    [Header("References")]
    [SerializeField] private Toggle _toggle;
    [SerializeField] private GameFramerate _gameFramerate;
    [SerializeField] private Slider _targetFramerateSlider;

    private SettingsProvider _settingsProvider;

    [Inject]
    private void Construct(SettingsProvider settingsProvider)
    {
        _settingsProvider = settingsProvider;
    }

    #region MonoBehaciour

    private void OnValidate()
    {
        _toggle ??= GetComponent<Toggle>();
    }

    private void Start()
    {
        UpdateValue();
    }

    private void OnEnable()
    {
        _toggle.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveListener(OnValueChanged);
    }

    #endregion

    public override void UpdateValue()
    {
        _toggle.isOn = _settingsProvider.settings.maxFramerateEnabled;
        OnValueChanged(_toggle.isOn);
    }

    private void OnValueChanged(bool state)
    {
        _targetFramerateSlider.interactable = !state;

        _gameFramerate.Set(state ? int.MaxValue : (int)_targetFramerateSlider.value);

        _settingsProvider.settings.maxFramerateEnabled = state;
    }
}
