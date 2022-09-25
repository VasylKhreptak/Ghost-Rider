
using System;
using UnityEngine;
public class DropdownMatchIndexEvent : MonoEvent
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
        _dropdownIndexEvents.onMatchIndex += OnMatchIndex;
    }

    private void OnDisable()
    {
        _dropdownIndexEvents.onMatchIndex -= OnMatchIndex;
    }

    #endregion

    private void OnMatchIndex()
    {
        onMonoCall?.Invoke();
    }
}
