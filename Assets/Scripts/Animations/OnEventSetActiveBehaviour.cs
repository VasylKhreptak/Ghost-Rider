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

    private void OnDisable()
    {
        _event.onMonoCall -= SetActive;
    }

    #endregion

    private void SetActive()
    {
        foreach (var script in _scripts)
        {
            script.enabled = _active;
        }
    }
}
