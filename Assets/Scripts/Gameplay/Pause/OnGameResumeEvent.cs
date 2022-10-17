using System.Collections;
using Zenject;
public class OnGameResumeEvent : MonoEvent
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
        _pauseEvents.onResume -= OnResume;
    }

    #endregion

    private IEnumerator AddListener()
    {
        yield return null;
        
        _pauseEvents.onResume += OnResume;
    }
    
    private void OnResume()
    {
        onMonoCall?.Invoke();
    }
}
