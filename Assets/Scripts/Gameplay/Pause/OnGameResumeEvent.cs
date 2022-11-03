using System.Collections;
using Zenject;
public class OnGameResumeEvent : MonoEvent
{
    private PauseManager _pauseManager;

    [Inject]
    private void Construct(PauseManager pauseManager)
    {
        this._pauseManager = pauseManager;
    }
    
    #region MonoBehaviour

    private void OnEnable()
    {
        StartCoroutine(AddListener());
    }

    private void OnDisable()
    {
        _pauseManager.onResume -= OnResume;
    }

    #endregion

    private IEnumerator AddListener()
    {
        yield return null;
        
        _pauseManager.onResume += OnResume;
    }
    
    private void OnResume()
    {
        onMonoCall?.Invoke();
    }
}
