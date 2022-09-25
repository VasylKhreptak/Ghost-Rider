using System;
using ModestTree;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AntiAliasingQualityDropdown : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera _camera;
    [SerializeField] private TMP_Dropdown _dropdown;

    private UniversalAdditionalCameraData _cameraData;

    private AntialiasingQuality[] _antialiasingQualities =
    {
        AntialiasingQuality.Low,
        AntialiasingQuality.Medium,
        AntialiasingQuality.High
    };

    #region MonoBehaviour

    private void OnValidate()
    {
        _camera ??= Camera.main;
        _dropdown ??= GetComponent<TMP_Dropdown>();
    }

    private void Awake()
    {
        _cameraData = _camera.GetComponent<UniversalAdditionalCameraData>();
        _dropdown.SetValueWithoutNotify(_antialiasingQualities.IndexOf(_cameraData.antialiasingQuality));
    }

    private void OnEnable()
    {
        _dropdown.onValueChanged.AddListener(SetQuality);
    }

    private void OnDisable()
    {
        _dropdown.onValueChanged.RemoveListener(SetQuality);
    }

    #endregion

    private void SetQuality(int index)
    {
        _cameraData.antialiasingQuality = _antialiasingQualities[index];
    }
}
