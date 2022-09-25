using System;
using UnityEngine;
using UnityEngine.UI;

public class PostProcessingToggle : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _globalVolume;
    [SerializeField] private Toggle _toggle;

    #region MonoBehaviour

    private void OnValidate()
    {
        _toggle ??= GetComponent<Toggle>();
    }

    private void Awake()
    {
        _toggle.SetIsOnWithoutNotify(_globalVolume.activeSelf);
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
        _globalVolume.SetActive(enabled);
    }
}
