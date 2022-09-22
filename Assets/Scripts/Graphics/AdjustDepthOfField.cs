using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class AdjustDepthOfField : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Volume _volume;

    private DepthOfField _depthOfField;
    
    #region MonBehaviour

    private void OnValidate()
    {
        _volume ??= GetComponent<Volume>();
    }


    private void Awake()
    {
        _volume.profile.TryGet(out _depthOfField);
    }

    #endregion

    public void SetActive(bool active)
    {
        _depthOfField.active = active;
    }
    
    public void SetMode(DepthOfFieldMode mode)
    {
        _depthOfField.mode.value = mode;
    }

    public void SetFocusDistance(float distance)
    {
        _depthOfField.focusDistance.value = distance;
    }

    public void SetFocalLength(int focalLength)
    {
        _depthOfField.focalLength.value = focalLength;
    }

    public void SetAperture(float aperture)
    {
        _depthOfField.aperture.value = aperture;
    }

    public void SetBladeCount(int count)
    {
        _depthOfField.bladeCount.value = count;
    }

    public void SetBladeCurvature(int curvature)
    {
        _depthOfField.bladeCurvature.value = curvature;
    }

    public void SetBladeRotation(float rotation)
    {
        _depthOfField.bladeRotation.value = rotation;
    }

    public void SetBokehMode()
    {
        _depthOfField.mode.value = DepthOfFieldMode.Bokeh;
    }

    public void SetGaussianMode()
    {
        _depthOfField.mode.value = DepthOfFieldMode.Gaussian;
    }

    public void SetNoneMode()
    {
        _depthOfField.mode.value = DepthOfFieldMode.Off;
    }
}
