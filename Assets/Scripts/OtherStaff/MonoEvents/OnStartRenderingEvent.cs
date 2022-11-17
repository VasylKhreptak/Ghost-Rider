using System;
using UnityEngine;

public class OnStartRenderingEvent : MonoEvent
{
    [Header("References")]
    [SerializeField] private DistanceRenderingEvents _distanceRenderingEvents;

    #region MonoBehaviour

    private void OnValidate()
    {
        _distanceRenderingEvents ??= GetComponent<DistanceRenderingEvents>();
    }

    private void Awake()
    {
        _distanceRenderingEvents.onStartRendering += Invoke;
    }

    private void OnDestroy()
    {
        _distanceRenderingEvents.onStartRendering -= Invoke;
    }

    #endregion
}
