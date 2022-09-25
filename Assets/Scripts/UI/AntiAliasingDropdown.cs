using ModestTree;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AntiAliasingDropdown : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera _camera;
    [SerializeField] private TMP_Dropdown _dropdown;

    private UniversalAdditionalCameraData _cameraData;

    private AntialiasingMode[] _antialiasingModes =
    {
        AntialiasingMode.None,
        AntialiasingMode.FastApproximateAntialiasing,
        AntialiasingMode.SubpixelMorphologicalAntiAliasing
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
        _dropdown.SetValueWithoutNotify(_antialiasingModes.IndexOf(_cameraData.antialiasing));
    }

    private void OnEnable()
    {
        _dropdown.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnDisable()
    {
        _dropdown.onValueChanged.RemoveListener(OnValueChanged);
    }

    #endregion

    private void OnValueChanged(int index)
    {
        SetAntiAliasing(index);
    }

    private void SetAntiAliasing(int index)
    {
        _cameraData.antialiasing = _antialiasingModes[index];        
    }
}
