using System;
using UnityEngine;

public class OnRaycastEvent : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CameraProvider _cameraProvider;
    
    [Header("Preferences")]
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _maxDistance;

    [Header("Event")]
    [SerializeField] private MonoEvent _monoEvent;

    public Action<RaycastHit> onRaycast;

    private Camera _camera;

    #region MonoBehaviour

    private void Awake()
    {
        _camera = _cameraProvider.Camera;

        _monoEvent.onMonoCall += ThrowRaycast;
    }

    private void OnDestroy()
    {
        _monoEvent.onMonoCall -= ThrowRaycast;
    }

    #endregion

    private void ThrowRaycast()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, _maxDistance, _layerMask))
        {
            onRaycast?.Invoke(hitInfo);
        }
    }
}
