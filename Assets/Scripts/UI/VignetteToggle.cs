using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class VignetteToggle : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Toggle _toggle;
    [SerializeField] private Volume _postProcessingVolume;

    private Vignette _vignette;

    #region MonoBehaviour

    private void OnValidate()
    {
        _toggle ??= GetComponent<Toggle>();
    }

    private void Awake()
    {
        _postProcessingVolume.profile.TryGet(out _vignette);
        _toggle.isOn = _vignette.IsActive();
    }

    private void OnEnable()
    {
        _toggle.onValueChanged.AddListener(SetVignetteState);
    }

    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveListener(SetVignetteState);
    }

    #endregion

    private void SetVignetteState(bool enabled)
    {
        _vignette.active = enabled;
    }
}
