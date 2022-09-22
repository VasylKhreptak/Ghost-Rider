using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class AdjustColor : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Volume _volume;

    private ColorAdjustments _colorAdjustments;

    #region MonoBehaviour

    private void OnValidate()
    {
        _volume ??= GetComponent<Volume>();
    }

    private void Awake()
    {
        _volume.profile.TryGet(out _colorAdjustments);
    }

    #endregion

    public void SetActive(bool active)
    {
        _colorAdjustments.active = active;
    }
    
    public void SetPostExposure(float exposure)
    {
        _colorAdjustments.postExposure.value = exposure;
    }

    public void SetContrast(float contrast)
    {
        _colorAdjustments.contrast.value = contrast;
    }

    public void SetColorFilter(ColorParameter color)
    {
        _colorAdjustments.colorFilter = color;
    }

    public void SetHueShift(float hue)
    {
        _colorAdjustments.hueShift.value = hue;
    }

    public void SetSaturation(float saturation)
    {
        _colorAdjustments.saturation.value = saturation;
    }
}
