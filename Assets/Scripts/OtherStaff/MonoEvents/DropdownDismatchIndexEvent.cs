using UnityEngine;

public class DropdownDismatchIndexEvent : MonoEvent
{
    [Header("References")]
    [SerializeField] private DropdownIndexEvents _dropdownIndexEvents;

    #region Monobehaviour

    private void OnValidate()
    {
        _dropdownIndexEvents ??= GetComponent<DropdownIndexEvents>();
    }

    private void OnEnable()
    {
        _dropdownIndexEvents.onDismatchIndex += OnDismatchIndex;
    }

    private void OnDisable()
    {
        _dropdownIndexEvents.onDismatchIndex -= OnDismatchIndex;
    }

    #endregion

    private void OnDismatchIndex()
    {
        onMonoCall?.Invoke();
    }
}
