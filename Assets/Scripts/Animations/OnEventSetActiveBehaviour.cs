using UnityEngine;

public class OnEventSetActiveBehaviour : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Behaviour[] _targetScripts;

    [Header("Preferences")]
    [SerializeField] private bool _active;
    
    [Header("MonoEvent")]
    [SerializeField] private MonoEvent _event;
    
    #region MonoBehaviour

    private void OnValidate()
    {
        _event ??= GetComponent<MonoEvent>();
    }

    private void OnEnable()
    {
        TryAddListener();
    }

    private void OnDisable()
    {
        _event.onMonoCall -= SetActive;
    }
    
    #endregion

    public void TryAddListener()
    {
        if (_event != null)
        {
            _event.onMonoCall += SetActive;
        }
    }
    
    public void SetScripts(Behaviour[] scripts)
    {
        _targetScripts = scripts;
    }

    public void SetTargetState(bool state)
    {
        _active = state;
    }

    public void SetEvent(MonoEvent monoEvent)
    {
        _event = monoEvent;
    }
    
    private void SetActive()
    {
        foreach (var script in _targetScripts)
        {
            script.enabled = _active;
        }
    }
}
