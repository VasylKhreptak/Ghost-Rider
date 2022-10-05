using UnityEngine;

public class OnToggleGroupMatchEvent : MonoEvent
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
        _toggleGroupEvents.onMatchCase += Invoke;
    }

    private void OnDisable()
    {
        _toggleGroupEvents.onMatchCase -= Invoke;
    }

    #endregion
}
