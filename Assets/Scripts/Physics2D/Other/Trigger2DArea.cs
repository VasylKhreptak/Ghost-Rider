using System.Collections.Generic;
using UnityEngine;

public class Trigger2DArea : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private OnTriggerEnter2DEvent _triggerEnterEvent;
    [SerializeField] private OnTriggerExit2DEvent _triggerExitEvent;

    public List<Collider2D> affectedObjects = new List<Collider2D>();

    public bool IsEmpty => affectedObjects.Count == 0;

    #region MonoBehaviour

    private void OnValidate()
    {
        _triggerEnterEvent ??= GetComponent<OnTriggerEnter2DEvent>();
        _triggerExitEvent ??= GetComponent<OnTriggerExit2DEvent>();
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

    private void OnEnter(Collider2D collider)
    {
        affectedObjects.Add(collider);
    }

    private void OnExit(Collider2D collider)
    {
        affectedObjects.Remove(collider);
    }
}
