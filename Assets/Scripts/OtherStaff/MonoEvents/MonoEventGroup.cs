using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonoEventGroup : MonoEvent
{
    [Header("Events")]
    [SerializeField] private MonoEvent[] _events;

    #region MonoBehaviour

    private void OnValidate()
    {
        if (_events == null)
        {
            _events = transform.GetComponents<MonoEvent>();
            List<MonoEvent> _eventsList = _events.ToList();
            _eventsList.Remove(this);
            _events = _eventsList.ToArray();
        }
    }

    private void OnEnable()
    {
        AddListeners();
    }

    private void OnDisable()
    {
        RemoveListeners();
    }

    #endregion

    private void AddListeners()
    {
        foreach (var @event in _events)
        {
            @event.onMonoCall += Invoke;
        }
    }

    private void RemoveListeners()
    {
        foreach (var @event in _events)
        {
            @event.onMonoCall -= Invoke;
        }
    }
}
