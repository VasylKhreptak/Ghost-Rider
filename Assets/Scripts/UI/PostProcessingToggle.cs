using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PostProcessingToggle : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Volume _volume;
    [SerializeField] private Toggle _toggle;

    #region MonoBehaviour

    private void OnValidate()
    {
        _toggle ??= GetComponent<Toggle>();
    }

    private void Awake()
    {
        _toggle.isOn = !Mathf.Approximately(_volume.weight, 0);
    }

    private void OnEnable()
    {
        _toggle.onValueChanged.AddListener(SetPostProcessingState);
    }

    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveListener(SetPostProcessingState);
    }

    #endregion

    private void SetPostProcessingState(bool enabled)
    {
        _volume.weight = enabled ? 1 : 0;
    }
}
