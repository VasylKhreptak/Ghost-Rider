using System.Collections;
using Zenject;
public class OnGamePauseEvent : MonoEvent
{
    private PauseEventsHolder _pauseEventsHolder;

    [Inject]
    private void Construct(PauseEventsHolder pauseEventsHolder)
    {
        _pauseEventsHolder = pauseEventsHolder;
    }
    
    #region MonoBehaviour
    
    private void OnEnable()
    {
        StartCoroutine(AddListener());
    }

    private void OnDisable()
    {
        _pauseEventsHolder.onPause -= OnPause;
    }

    #endregion

    private IEnumerator AddListener()
    {
        yield return null;
        
        _pauseEventsHolder.onPause += OnPause;
    }
    
    private void OnPause()
    {
        onMonoCall?.Invoke();
    }
}
