using System;
using System.Collections;
using UnityEngine;

public class ConfigurableUpdate
{
    private MonoBehaviour _targetScript;
    private float _updateDelay;
    private Action _action;

    private bool _init;
    
    private Coroutine _updateCoroutine;

    public void Init(MonoBehaviour monoBehaviour, float updateDelay, Action action)
    {
        _targetScript = monoBehaviour;
        _updateDelay = updateDelay;
        _action = action;

        _init = true;
    }

    public void StartUpdating()
    {
        if (_init == false)
        {
            Debug.LogWarning("ConfigurableUpdate was not initialized, use Init()");
        }
        
        if (_updateCoroutine == null)
        {
            _updateCoroutine = _targetScript.StartCoroutine(UpdateRoutine());
        }
    }

    public void StopUpdating()
    {
        if (_updateCoroutine != null)
        {
            _targetScript.StopCoroutine(_updateCoroutine);

            _updateCoroutine = null;
        }
    }

    private IEnumerator UpdateRoutine()
    {
        while (true)
        {
            _action?.Invoke();

            yield return new WaitForSeconds(_updateDelay);
        }
    }
}
