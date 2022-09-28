using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class ResolutionDropdown : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Dropdown _dropdown;

    private SettingsProvider _settingsProvider;
    
    private Resolution[] _resolutions;

    [Inject]
    private void Construct(SettingsProvider settingsProvider)
    {
        _settingsProvider = settingsProvider;
    }
    
    #region MonoBehaviour

    private void Start()
    {
        _resolutions = Screen.resolutions;

        Array.Reverse(_resolutions);
        
        Init();
    }

    private void OnValidate()
    {
        _dropdown ??= GetComponent<TMP_Dropdown>();
    }

    private void OnEnable()
    {
        _dropdown.onValueChanged.AddListener(SetResolution);
    }

    private void OnDisable()
    {
        _dropdown.onValueChanged.RemoveListener(SetResolution);
    }

    #endregion

    private void Init()
    {
        _dropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (var i = 0; i < _resolutions.Length; i++)
        {
            var resolution = _resolutions[i];
            options.Add(resolution.width + " x " + resolution.height + " " + resolution.refreshRate + " Hz");

            if (Screen.currentResolution.width == resolution.width && Screen.currentResolution.height == resolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        _dropdown.AddOptions(options);

        _dropdown.value = currentResolutionIndex;
        _dropdown.RefreshShownValue();
    }

    private void SetResolution(int index)
    {
        Resolution resolution = _resolutions[index];
        
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode, resolution.refreshRate);

        _settingsProvider.settings.screenWidth = resolution.width;
        _settingsProvider.settings.screenHeight = resolution.height;
        _settingsProvider.settings.screenRefreshRate = resolution.refreshRate;
    }
}
