using System.Collections;
using Zenject;
public class OnGamePauseEvent : MonoEvent
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
        _pauseManager.onPause -= OnPause;
    }

    #endregion

    private IEnumerator AddListener()
    {
        yield return null;
        
        _pauseManager.onPause += OnPause;
    }
    
    private void OnPause()
    {
        onMonoCall?.Invoke();
    }
}
