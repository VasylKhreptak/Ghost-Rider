using UnityEngine;

public class OnToggleGroupDismatchEvent : MonoEvent
{
    [Header("References")]
    [SerializeField] private ToggleGroupEvents _toggleGroupEvents;

    #region MonoBehaivour

    private void OnValidate()
    {
        _toggleGroupEvents ??= GetComponent<ToggleGroupEvents>();
    }

    private void OnEnable()
    {
        _toggleGroupEvents.onDismatchCase += Invoke;
    }

    private void OnDisable()
    {
        _toggleGroupEvents.onDismatchCase -= Invoke;
    }

    #endregion

    private void Invoke()
    {
        onMonoCall?.Invoke();
    }
}
