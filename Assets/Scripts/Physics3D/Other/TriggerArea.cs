using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TriggerArea : MonoBehaviour
{
    [Header("References")]
    [FormerlySerializedAs("_triggerEnterEvent")] public OnTriggerEnterEvent triggerEnterEvent;
    [FormerlySerializedAs("_triggerExitEvent")] public OnTriggerExitEvent triggerExitEvent;

    public List<Collider> affectedObjects = new List<Collider>();

    public Action onFill;
    public Action onEmpty;

    public bool IsEmpty => affectedObjects.Count == 0;

    #region MonoBehaviour

    private void OnValidate()
    {
        triggerEnterEvent ??= GetComponent<OnTriggerEnterEvent>();
        triggerExitEvent ??= GetComponent<OnTriggerExitEvent>();
    }

    private void OnEnable()
    {
        triggerEnterEvent.onEnter += OnEnter;
        triggerExitEvent.onExit += OnExit;
    }

    private void OnDisable()
    {
        triggerEnterEvent.onEnter -= OnEnter;
        triggerExitEvent.onExit -= OnExit;
    }

    #endregion

    private void OnEnter(Collider collider)
    {
        affectedObjects.Add(collider);

        if (affectedObjects.Count == 1)
        {
            onFill?.Invoke();
        }
    }

    private void OnExit(Collider collider)
    {
        affectedObjects.Remove(collider);
        
        if(affectedObjects.Count == 0)
        {
            onEmpty?.Invoke();
        }
    }
}
