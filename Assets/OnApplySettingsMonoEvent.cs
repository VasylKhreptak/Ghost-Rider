using System;
using UnityEngine;

public class OnApplySettingsMonoEvent : MonoEvent
{
    [Header("References")]
    [SerializeField] private OnEventApplySettings _onEventApplySettings;

    #region MonoBehaviour

    private void OnValidate()
    {
        _onEventApplySettings ??= GetComponent<OnEventApplySettings>();
    }

    private void OnEnable()
    {
        _onEventApplySettings.onApply += OnApply;
    }

    private void OnDisable()
    {
        _onEventApplySettings.onApply -= OnApply;
    }

    #endregion

    private void OnApply()
    {
        onMonoCall?.Invoke();
    }
}
