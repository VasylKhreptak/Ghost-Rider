using System;
using UnityEngine;

public class OnEventSetActive : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _gameObject;

    [Header("Preferences")]
    [SerializeField] private bool _active;
    
    [Header("MonoEvent")]
    [SerializeField] private MonoEvent _monoEvent;
    
    #region MonoBehaviour

    private void OnValidate()
    {
        _gameObject ??= gameObject;
    }

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
        _gameObject.SetActive(_active);
    }
}
