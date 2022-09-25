using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class DepthOfFieldToggle : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Toggle _toggle;
    [SerializeField] private Volume _postProcessingVolume;

    private DepthOfField _depthOfField;

    #region MonoBehaviour

    private void OnValidate()
    {
        _toggle ??= GetComponent<Toggle>();
    }

    private void Awake()
    {
        _postProcessingVolume.profile.TryGet(out _depthOfField);
        _toggle.isOn = _depthOfField.IsActive();
    }

    private void OnEnable()
    {
        _toggle.onValueChanged.AddListener(SetDepthOfFieldState);
    }

    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveListener(SetDepthOfFieldState);
    }

    #endregion

    private void SetDepthOfFieldState(bool enabled)
    {
        _depthOfField.active = enabled;
    }
}
