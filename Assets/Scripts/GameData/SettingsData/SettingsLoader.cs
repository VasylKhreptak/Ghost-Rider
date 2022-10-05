using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Zenject;
using Bloom = UnityEngine.Rendering.Universal.Bloom;
using DepthOfField = UnityEngine.Rendering.Universal.DepthOfField;
using Vignette = UnityEngine.Rendering.Universal.Vignette;

public class SettingsLoader : MonoBehaviour
{
    [Header("Audio References")]
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private AudioMixerGroup _masterMixerGroup;
    [SerializeField] private AudioMixerGroup _musicMixerGroup;
    [SerializeField] private AudioMixerGroup _soundMixerGroup;
    [SerializeField] private AudioMixerGroup _carsMixerGroup;

    [Header("UI Preferences")]
    [SerializeField] private GameObject _fpsCounterObject;
    
    [Header("Input References")]
    [SerializeField] private RCC_Camera _rccCamera;

    [Header("Graphics References")]
    [SerializeField] private GameFramerate _gameFramerate;
    [SerializeField] private Camera _camera;
    [SerializeField] private Volume _postProcessingVolume;

    private UniversalAdditionalCameraData _cameraData;

    private UniversalRenderPipelineAsset _pipelineAsset;

    private SettingsProvider _settingsProvider;

    private Bloom _bloom;
    private Vignette _vignette;
    private DepthOfField _depthOfField;

    [Inject]
    private void Construct(SettingsProvider settingsProvider)
    {
        _settingsProvider = settingsProvider;
    }

    #region MonoBehaviour

    private void Awake()
    {
        _cameraData = _camera.GetComponent<UniversalAdditionalCameraData>();
        _pipelineAsset = (UniversalRenderPipelineAsset)GraphicsSettings.currentRenderPipeline;

        _postProcessingVolume.profile.TryGet(out _bloom);
        _postProcessingVolume.profile.TryGet(out _vignette);
        _postProcessingVolume.profile.TryGet(out _depthOfField);
    }

    private void Start()
    {
        ApplySettings(_settingsProvider.settings);
    }

    #endregion

    private void ApplySettings(Settings settings)
    {
        ApplyVolumeSettings(settings);
        ApplyUISettings(settings);
        ApplyInputSettings(settings);
        ApplyScreenSettings(settings);
        ApplyGraphicsSettings(settings);
    }

    public void ApplyVolumeSettings(Settings settings)
    {
        _audioMixer.SetFloat(_masterMixerGroup.name, settings.masterMixerVolume);
        _audioMixer.SetFloat(_musicMixerGroup.name, settings.musicMixerVolume);
        _audioMixer.SetFloat(_soundMixerGroup.name, settings.soundMixerVolume);
        _audioMixer.SetFloat(_carsMixerGroup.name, settings.carsMixerVolume);

        ApplyVolumeSettingsToProvider(settings);
    }

    public void ApplyUISettings(Settings settings)
    {
        _fpsCounterObject.SetActive(settings.showFPS);
        
        ApplyUISettingsToProvider(settings);
    }

    private void ApplyUISettingsToProvider(Settings settings)
    {
        _settingsProvider.settings.showFPS = settings.showFPS;
    }
    
    public void ApplyInputSettings(Settings settings)
    {
        _rccCamera.sensitivity = settings.mouseSensitivity;

        ApplyInputSettingsToProvider(settings);
    }

    public void ApplyGraphicsSettings(Settings settings)
    {
        QualitySettings.vSyncCount = settings.vSyncEnabled ? 1 : 0;
        _camera.farClipPlane = settings.cameraClipping;
        _postProcessingVolume.weight = settings.postProcessingEnabled ? 1 : 0;
        _cameraData.antialiasing = settings.antialiasingMode;
        _cameraData.antialiasingQuality = settings.antialiasingQuality;
        _pipelineAsset.msaaSampleCount = settings.msaaSampleCount;
        _pipelineAsset.renderScale = settings.renderScale;
        _pipelineAsset.upscalingFilter = settings.upscalingFilter;

        _pipelineAsset.supportsHDR = settings.hdrEnabled;
        _bloom.active = settings.bloomEnabled;
        _vignette.active = settings.vignetteEnabled;
        _depthOfField.active = settings.depthOfFieldEnabled;
        _depthOfField.highQualitySampling.value = settings.dofHighQualityEnabled;
        _bloom.highQualityFiltering.value = settings.bloomHighQualityEnabled;

        QualitySettings.masterTextureLimit = settings.masterTextureLimit;

        ApplyGraphicsSettingsToProvider(settings);
    }

    private void ApplyVolumeSettingsToProvider(Settings settings)
    {
        _settingsProvider.settings.masterMixerVolume = settings.masterMixerVolume;
        _settingsProvider.settings.musicMixerVolume = settings.musicMixerVolume;
        _settingsProvider.settings.soundMixerVolume = settings.soundMixerVolume;
        _settingsProvider.settings.carsMixerVolume = settings.carsMixerVolume;
    }

    private void ApplyInputSettingsToProvider(Settings settings)
    {
        _settingsProvider.settings.mouseSensitivity = settings.mouseSensitivity;
    }

    private void ApplyGraphicsSettingsToProvider(Settings settings)
    {
        _settingsProvider.settings.vSyncEnabled = settings.vSyncEnabled;
        _settingsProvider.settings.cameraClipping = settings.cameraClipping;
        _settingsProvider.settings.postProcessingEnabled = settings.postProcessingEnabled;
        _settingsProvider.settings.antialiasingMode = settings.antialiasingMode;
        _settingsProvider.settings.msaaSampleCount = settings.msaaSampleCount;
        _settingsProvider.settings.antialiasingQuality = settings.antialiasingQuality;
        _settingsProvider.settings.renderScale = settings.renderScale;
        _settingsProvider.settings.upscalingFilter = settings.upscalingFilter;
        _settingsProvider.settings.hdrEnabled = settings.hdrEnabled;
        _settingsProvider.settings.bloomEnabled = settings.bloomEnabled;
        _settingsProvider.settings.vignetteEnabled = settings.vignetteEnabled;
        _settingsProvider.settings.depthOfFieldEnabled = settings.depthOfFieldEnabled;
        _settingsProvider.settings.dofHighQualityEnabled = settings.dofHighQualityEnabled;
        _settingsProvider.settings.bloomHighQualityEnabled = settings.bloomHighQualityEnabled;
        _settingsProvider.settings.masterTextureLimit = settings.masterTextureLimit;
    }

    public void ApplyScreenSettings(Settings settings)
    {
        Screen.SetResolution(settings.screenWidth, settings.screenHeight, settings.fullScreenMode, settings.screenRefreshRate);
        _gameFramerate.Set(settings.targetFramerate);

        if (settings.maxFramerateEnabled)
        {
            _gameFramerate.Set(int.MaxValue);
        }
        
        ApplyScreenSettingsToProvider(settings);
    }

    private void ApplyScreenSettingsToProvider(Settings settings)
    {
        _settingsProvider.settings.fullScreenMode = settings.fullScreenMode;
        _settingsProvider.settings.screenWidth = settings.screenWidth;
        _settingsProvider.settings.screenHeight = settings.screenHeight;
        _settingsProvider.settings.screenRefreshRate = settings.screenRefreshRate;
        _settingsProvider.settings.targetFramerate = settings.targetFramerate;
        _settingsProvider.settings.maxFramerateEnabled = settings.maxFramerateEnabled;
    }
}
