using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[Serializable]
public class Settings
{
	public float masterMixerVolume = 0f;
	public float musicMixerVolume = 0f;
	public float soundMixerVolume = 0f;
	public float carsMixerVolume = 0f;

	public bool showFPS = true;
	
	public float mouseSensitivity = 1;

	public FullScreenMode fullScreenMode = FullScreenMode.FullScreenWindow;
	public int screenWidth = 1920;
	public int screenHeight = 1080;
	public int screenRefreshRate = 60;//подивитися чи це використовується в коді
	public int targetFramerate = 60;
	public bool maxFramerateEnabled = false;
	public bool vSyncEnabled = false;
	public float cameraClipping = 600f;
	public bool postProcessingEnabled = true;
	public AntialiasingMode antialiasingMode = AntialiasingMode.FastApproximateAntialiasing;
	public int msaaSampleCount = 1;
	public AntialiasingQuality antialiasingQuality = AntialiasingQuality.Low;
	public float renderScale = 1f;
	public UpscalingFilterSelection upscalingFilter = UpscalingFilterSelection.Linear;
	public bool hdrEnabled = true;
	public bool bloomEnabled = true;
	public bool vignetteEnabled = true;
	public bool depthOfFieldEnabled = true;
	public bool dofHighQualityEnabled = true;
	public bool bloomHighQualityEnabled = true;
	public int masterTextureLimit = 0;
}
