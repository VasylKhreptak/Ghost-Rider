using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResolutionDropdown : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Dropdown _dropdown;

    private Resolution[] _resolutions;

    #region MonoBehaviour

    private void Awake()
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
            options.Add(resolution.width + " x " + resolution.height);

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
        Screen.SetResolution(_resolutions[index].width, _resolutions[index].height, Screen.fullScreenMode);
    }
}
