using UnityEngine;

public class OnEventSetActiveBehaviour : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MonoBehaviour[] _scripts;

    [Header("Preferences")]
    [SerializeField] private bool _active;
    
    [Header("MonoEvent")]
    [SerializeField] private MonoEvent _event;
    
    
    #region MonoBehaviour

    private void OnValidate()
    {
        _event ??= GetComponent<MonoEvent>();
    }

    private void Awake()
    {
        _event.onMonoCall += SetActive;
    }

    private void OnDestroy()
    {
        _event.onMonoCall -= SetActive;
    }
    
    #endregion

    public void SetScripts(MonoBehaviour[] scripts)
    {
        _scripts = scripts;
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
        foreach (var script in _scripts)
        {
            script.enabled = _active;
        }
    }
}
