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
        ApplyInputSettings(settings);
        ApplyGraphicsSettings(settings);
    }

    private void ApplyVolumeSettings(Settings settings)
    {
        _audioMixer.SetFloat(_masterMixerGroup.name, settings.masterMixerVolume);
        _audioMixer.SetFloat(_musicMixerGroup.name, settings.musicMixerVolume);
        _audioMixer.SetFloat(_soundMixerGroup.name, settings.soundMixerVolume);
        _audioMixer.SetFloat(_carsMixerGroup.name, settings.carsMixerVolume);
    }

    private void ApplyInputSettings(Settings settings)
    {
        _rccCamera.sensitivity = settings.mouseSensitivity;
    }

    private void ApplyGraphicsSettings(Settings settings)
    {
        Screen.SetResolution(settings.screenWidth, settings.screenHeight, settings.fullScreenMode);
        _gameFramerate.Set(settings.targetFramerate);

        if (settings.maxFramerateEnabled)
        {
            _gameFramerate.Set(int.MaxValue);
        }

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

        QualitySettings.vSyncCount = settings.vSyncEnabled ? 1 : 0;
        QualitySettings.masterTextureLimit = settings.masterTextureLimit;
    }
}
