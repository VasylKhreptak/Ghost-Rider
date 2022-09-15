using UnityEngine;

public class OnEventSetActive : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] _gameObjects;

    [Header("Preferences")]
    [SerializeField] private bool _active;
    
    [Header("MonoEvent")]
    [SerializeField] private MonoEvent _monoEvent;
    
    #region MonoBehaviour

    private void Awake()
    {
        _monoEvent.onMonoCall += SetActive;
    }

    private void OnDestroy()
    {
        _monoEvent.onMonoCall -= SetActive;
    }

    #endregion

    private void SetActive()
    {
        foreach (var obj in _gameObjects)
        {
            obj.SetActive(_active);
        }
    }
}
