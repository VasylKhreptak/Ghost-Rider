using System;
using TMPro;
using UnityEngine;

public class DropdownIndexEvents : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Dropdown _dropdown;

    [Header("Preferences")]
    [SerializeField] private int _targetIndex;

    public Action onMatchIndex;
    public Action onDismatchIndex;

    #region MonoBehaviour

    private void OnValidate()
    {
        _dropdown ??= GetComponent<TMP_Dropdown>();
    }

    private void OnEnable()
    {
        _dropdown.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnDisable()
    {
        _dropdown.onValueChanged.RemoveListener(OnValueChanged);
    }

    #endregion

    private void OnValueChanged(int index)
    {
        (index == _targetIndex ? onMatchIndex : onDismatchIndex)?.Invoke();
    }
}
