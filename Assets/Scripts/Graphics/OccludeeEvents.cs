using System;
using UnityEngine;

public class OccludeeEvents : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _transform;
    
    [Header("Preferences")]
    [SerializeField] private float _checkDelay;
    [SerializeField] private float _maxRenderDistance;

    private ConfigurableUpdate _configurableUpdate = new ConfigurableUpdate();
    
    private Camera _camera;

    private bool _visible = true;
    
    public Action onBecameInvisible;
    public Action onBecameVisible;

    #region MonoBehaviour

    private void OnValidate()
    {
        _transform ??= GetComponent<Transform>();
    }

    private void Awake()
    {
        _camera = Camera.main;
        
        _configurableUpdate.Init(this, _checkDelay, Check);
    }

    private void OnEnable()
    {
        _configurableUpdate.StartUpdating();
    }

    private void OnDisable()
    {
        _configurableUpdate.StopUpdating();
    }

    #endregion

    private void Check()
    {
        float distance = Vector3.Distance(_transform.position, _camera.transform.position);

        if (distance > _maxRenderDistance)
        {
            if (_visible)
            {
                onBecameInvisible?.Invoke();
                _visible = false;
            }
        }
        else
        {
            if (_visible == false)
            {
                onBecameVisible?.Invoke();
                _visible = true;
            }
        }
            
    }
}
