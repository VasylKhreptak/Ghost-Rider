using System.Collections;
using UnityEngine;
using Zenject;
public class OnGameResumeEvent : MonoEvent
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
        _pauseEventsHolder.onResume -= OnResume;
    }

    #endregion

    private IEnumerator AddListener()
    {
        yield return null;
        
        _pauseEventsHolder.onResume += OnResume;
    }
    
    private void OnResume()
    {
        onMonoCall?.Invoke();
    }
}
