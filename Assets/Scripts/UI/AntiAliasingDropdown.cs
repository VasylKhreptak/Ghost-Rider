using ModestTree;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Zenject;

public class AntiAliasingDropdown : UIUpdatableItem
{
    [Header("References")]
    [SerializeField] private CameraProvider _cameraProvider;
    [SerializeField] private TMP_Dropdown _dropdown;

    private UniversalAdditionalCameraData _cameraData;

    private SettingsProvider _settingsProvider;

    private AntialiasingMode[] _antialiasingModes =
    {
        AntialiasingMode.None,
        AntialiasingMode.FastApproximateAntialiasing,
        AntialiasingMode.SubpixelMorphologicalAntiAliasing
    };
    
    private Camera _camera;

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

    private void Awake()
    {
        _camera = _cameraProvider.Camera;
    }

    private void Start()
    {
        _cameraData = _camera.GetComponent<UniversalAdditionalCameraData>();

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

    #endregion

    public override void UpdateValue()
    {
        _dropdown.value = _antialiasingModes.IndexOf(_settingsProvider.settings.antialiasingMode);
    }

    private void OnValueChanged(int index)
    {
        SetAntiAliasing(index);
    }

    private void SetAntiAliasing(int index)
    {
        _cameraData.antialiasing = _antialiasingModes[index];

        _settingsProvider.settings.antialiasingMode = _antialiasingModes[index];
    }
}
