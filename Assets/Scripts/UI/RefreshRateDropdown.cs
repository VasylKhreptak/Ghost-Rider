using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Zenject;

public class RefreshRateDropdown : UIUpdatableItem
{
    [Header("References")]
    [SerializeField] private TMP_Dropdown _dropdown;

    private SettingsProvider _settingsProvider;

    private Resolution[] _resolutions;

    private List<int> _refreshRates = new List<int>();

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

    private void Start()
    {
        UpdateValue();
    }

    private void OnEnable()
    {
        _dropdown.onValueChanged.AddListener(SetRefreshRate);
    }

    private void OnDisable()
    {
        _dropdown.onValueChanged.RemoveListener(SetRefreshRate);
    }

    #endregion

    public override void UpdateValue()
    {
        _resolutions = Screen.resolutions;

        InitDropdown();
    }

    private void SetRefreshRate(int index)
    {
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, 
            Screen.fullScreenMode, _refreshRates[index]);

        _settingsProvider.settings.screenRefreshRate = _refreshRates[index];
    }

    private void InitDropdown()
    {
        _dropdown.ClearOptions();

        _refreshRates = GetPossibleRefreshRates(ref _resolutions);

        List<string> options = new List<string>();
        options = _refreshRates.ConvertAll(x => x + " hz");
        
        _dropdown.AddOptions(options);
        _dropdown.SetValueWithoutNotify(_refreshRates.IndexOf(Screen.currentResolution.refreshRate));
        _dropdown.RefreshShownValue();
    }

    public List<int> GetPossibleRefreshRates(ref Resolution[] resolutions)
    {
        List<int> possibleRefreshRates = resolutions
            .GroupBy(x => x.refreshRate).Select(x => x.First()).Select(x => x.refreshRate * 1).ToList();

        return possibleRefreshRates;
    }
}
