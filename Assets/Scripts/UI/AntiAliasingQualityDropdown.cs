using ModestTree;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Zenject;

public class AntiAliasingQualityDropdown : UIUpdatableItem
{
    [Header("References")]
    [SerializeField] private CameraProvider _cameraProvider;
    [SerializeField] private TMP_Dropdown _dropdown;

    private UniversalAdditionalCameraData _cameraData;

    private SettingsProvider _settingsProvider;

    private Camera _camera;
    
    private AntialiasingQuality[] _antialiasingQualities =
    {
        AntialiasingQuality.Low,
        AntialiasingQuality.Medium,
        AntialiasingQuality.High
    };

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
        _dropdown.onValueChanged.AddListener(SetQuality);
    }

    private void OnDisable()
    {
        _dropdown.onValueChanged.RemoveListener(SetQuality);
    }

    #endregion

    public override void UpdateValue()
    {
        _dropdown.SetValueWithoutNotify(_antialiasingQualities.IndexOf(_settingsProvider.settings.antialiasingQuality));

        _dropdown.interactable = _settingsProvider.settings.antialiasingMode == AntialiasingMode.SubpixelMorphologicalAntiAliasing;
    }

    private void SetQuality(int index)
    {
        _cameraData.antialiasingQuality = _antialiasingQualities[index];

        _settingsProvider.settings.antialiasingQuality = _antialiasingQualities[index];
    }
}
