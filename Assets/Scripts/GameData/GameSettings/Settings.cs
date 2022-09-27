using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[Serializable]
public class Settings
{
	public float masterVolume = 0f;
	public float musicVolume = 0f;
	public float soundVolume = 0f;
	public float carsVolume = 0f;
	
	public float mouseSensitivity = 1;

	public FullScreenMode fullScreenMode = FullScreenMode.FullScreenWindow;
	public Resolution resolution;
	public int targetFramerate = 60;
	public bool maxFramerateEnabled = false;
	public bool vsyncEnabled = false;
	public float cameraClipping = 600f;
	public bool postProcessingEnabled = true;
	public AntialiasingMode antialiasingMode = AntialiasingMode.FastApproximateAntialiasing;
	public int msaaIndex = 1;
	public AntialiasingQuality antialiasingQuality = AntialiasingQuality.Low;
	public float renderScale = 1f;
	public UpscalingFilterSelection upscalingFilter = UpscalingFilterSelection.Linear;
	public bool hdrEnabled = true;
	public bool bloomEnabled = true;
	public bool vignetteEnabled = true;
	public bool depthOfFieldEnabled = true;
	public bool _dofHighQualityEnabled = true;
	public int textureResolutionIndex = 0;
}
