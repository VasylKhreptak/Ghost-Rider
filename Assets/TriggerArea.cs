using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private OnTriggerEnterEvent _triggerEnterEvent;
    [SerializeField] private OnTriggerExitEvent _triggerExitEvent;

    public List<Collider> affectedObjects = new List<Collider>();

    public bool IsEmpty => affectedObjects.Count == 0;
    
    #region MonoBehaviour

    private void OnValidate()
    {
        _triggerEnterEvent ??= GetComponent<OnTriggerEnterEvent>();
        _triggerExitEvent ??= GetComponent<OnTriggerExitEvent>();
    }

    private void OnEnable()
    {
        _triggerEnterEvent.onEnter += OnEnter;
        _triggerExitEvent.onExit += OnExit;
    }

    private void OnDisable()
    {
        _triggerEnterEvent.onEnter -= OnEnter;
        _triggerExitEvent.onExit -= OnExit;
    }

    #endregion

    private void OnEnter(Collider collider)
    {
        affectedObjects.Add(collider);
    }

    private void OnExit(Collider collider)
    {
        affectedObjects.Remove(collider);
    }
}
