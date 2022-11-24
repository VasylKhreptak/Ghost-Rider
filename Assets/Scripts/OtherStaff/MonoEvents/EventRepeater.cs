using UnityEngine;

public class EventRepeater : MonoEvent
{
    [Header("Events")]
    [SerializeField] private MonoEvent _event;

    #region MonoBehaviour

    private void OnEnable()
    {
        _event.onMonoCall += Invoke;
    }

    private void OnDisable()
    {
        _event.onMonoCall -= Invoke;
    }

    #endregion
}
