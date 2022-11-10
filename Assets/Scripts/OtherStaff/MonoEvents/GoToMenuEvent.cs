using UnityEngine;
using Zenject;

public class GoToMenuEvent : MonoEvent
{
    [Inject(Id = MonoEvents.OnGoToMenu)] private MonoEvent _event;

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
