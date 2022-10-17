using UnityEngine;
using UnityEngine.UI;

public class OnToggleStateMonoEvent : MonoEvent
{
    [Header("References")]
    [SerializeField] private Toggle _toggle;

    [Header("Preferences")]
    [SerializeField] private bool _state;

    #region MonoBehaviour

    private void OnValidate()
    {
        _toggle ??= GetComponent<Toggle>();
    }
    
    private void OnEnable()
    {
        _toggle.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveListener(OnValueChanged);
    }

    #endregion

    private void OnValueChanged(bool value)
    {
        if (value == _state)
        {
            onMonoCall?.Invoke();
        }
    }
}
