using System.Collections;
using Zenject;
public class OnGamePauseEvent : MonoEvent
{
    private PauseEvents _pauseEvents;

    [Inject]
    private void Construct(PauseEvents _pauseEvents)
    {
        this._pauseEvents = _pauseEvents;
    }
    
    #region MonoBehaviour
    
    private void OnEnable()
    {
        StartCoroutine(AddListener());
    }

    private void OnDisable()
    {
        _pauseEvents.onPause -= OnPause;
    }

    #endregion

    private IEnumerator AddListener()
    {
        yield return null;
        
        _pauseEvents.onPause += OnPause;
    }
    
    private void OnPause()
    {
        onMonoCall?.Invoke();
    }
}
