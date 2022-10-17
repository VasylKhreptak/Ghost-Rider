using System;
using UnityEngine;

public class PauseEvents : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MonoEvent _toggleEvent;

    public bool isPaused { get; private set; }

    public Action onPause;
    public Action onResume;

    #region MonoBehaviour

    private void OnValidate()
    {
        _toggleEvent ??= GetComponent<MonoEvent>();
    }

    private void OnEnable()
    {
        _toggleEvent.onMonoCall += OnToggle;
    }

    private void OnDisable()
    {
        _toggleEvent.onMonoCall -= OnToggle;
    }

    #endregion

    private void OnToggle()
    {
        isPaused = !isPaused;
        (isPaused ? onPause : onResume)?.Invoke();
    }
}
