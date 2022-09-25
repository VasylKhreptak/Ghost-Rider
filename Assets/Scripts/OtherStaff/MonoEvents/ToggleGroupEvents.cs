using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleGroupEvents : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private List<ToggleItem> _toggleItems = new List<ToggleItem>();

    [Header("Event")]
    [SerializeField] private MonoEvent _checkEvent;

    public Action onMatchCase;
    public Action onDismatchCase;

    #region MonoBehaviour

    private void OnValidate()
    {
        if (_toggleItems == null)
        {
            SyncContent();
        }
    }

    private void OnEnable()
    {
        _checkEvent.onMonoCall += CheckState;
    }

    private void OnDisable()
    {
        _checkEvent.onMonoCall -= CheckState;
    }

    #endregion
    
    [ContextMenu("SyncContent")]
    private void SyncContent()
    {
        _toggleItems.Clear();

        Toggle[] toggles = transform.GetComponentsInChildren<Toggle>();

        foreach (var toggle in toggles)
        {
            ToggleItem toggleItem = new ToggleItem();

            toggleItem.toggle = toggle;

            _toggleItems.Add(toggleItem);
        }
    }
    
    private void CheckState()
    {
        (MatchCase() ? onMatchCase : onDismatchCase)?.Invoke();
    }

    private bool MatchCase()
    {
        bool match = true;

        for (int i = 0; i < _toggleItems.Count; i++)
        {
            match = match && (_toggleItems[i].@case == _toggleItems[i].toggle.isOn);
        }

        return match;
    }

    [Serializable]
    private class ToggleItem
    {
        public Toggle toggle;
        public bool @case;
    }
}
