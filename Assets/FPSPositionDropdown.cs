using System.Linq;
using ModestTree;
using TMPro;
using UnityEngine;
using Zenject;

public class FPSPositionDropdown : UIUpdatableItem
{
    [Header("References")]
    [SerializeField] private TMP_Dropdown _dropdown;
    [SerializeField] private RectTransform _FPSRectTransform;

    [Header("Preferences")]
    [SerializeField] private AnchorPreset[] _anchorPresets;

    private SettingsProvider _settingsProvider;

    [Inject]
    private void Construct(SettingsProvider settingsProvider)
    {
        _settingsProvider = settingsProvider;
    }

    #region MonoBehaviour

    private void OnValidate()
    {
        _dropdown ??= GetComponent<TMP_Dropdown>();
    }

    #endregion

    private void Start()
    {
        UpdateValue();
    }

    private void OnEnable()
    {
        _dropdown.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnDisable()
    {
        _dropdown.onValueChanged.RemoveListener(OnValueChanged);
    }
    
    public override void UpdateValue()
    {
        int index = _anchorPresets
            .IndexOf(_anchorPresets.First(x => x.min == _settingsProvider.settings.fpsAnchorPosition.min
                && x.max == _settingsProvider.settings.fpsAnchorPosition.max
                && x.anchoredPosition == _settingsProvider.settings.fpsAnchorPosition.anchoredPosition));

        _dropdown.SetValueWithoutNotify(index);

        _FPSRectTransform.SetAnchorPreset(_settingsProvider.settings.fpsAnchorPosition);
        _dropdown.interactable = _settingsProvider.settings.showFPS;
    }

    private void OnValueChanged(int index)
    {
        AnchorPreset anchorPreset = _anchorPresets[index];

        _FPSRectTransform.SetAnchorPreset(anchorPreset);

        _settingsProvider.settings.fpsAnchorPosition = anchorPreset;
    }
}
