using System;
using System.Diagnostics;
using NaughtyAttributes;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class DistanceRenderingEvents : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _transform;
    [HideIf(nameof(_useMainCamera)), SerializeField]
    private Camera _camera;

    [Header("Preferences")]
    [SerializeField] private float _renderDistance;
    [SerializeField] private bool _useMainCamera;

    [Header("Events")]
    [SerializeField] private MonoEvent _checkEvent;

    private bool _wasVisible;
    private bool _isVisible;

    public Action onStartRendering;
    public Action onStopRendering;

    #region MonoBehavior

    private void OnValidate()
    {
        _transform ??= GetComponent<Transform>();
        _checkEvent ??= GetComponent<MonoEvent>();

        if (_useMainCamera)
        {
            _camera = Camera.main;
        }
    }

    private void Awake()
    {
        if (_camera == null && _useMainCamera)
        {
            _camera = Camera.main;
        }
    }

    private void OnEnable()
    {
        _checkEvent.onMonoCall += CheckVisibility;

        CheckSpawnedInvisibly();
    }

    private void OnDisable()
    {
        _checkEvent.onMonoCall -= CheckVisibility;
    }

    #endregion

    private void CheckVisibility()
    {
        UpdateIsVisibleVariable();

        if (_isVisible && _wasVisible == false)
        {
            onStartRendering?.Invoke();
        }
        else if (_isVisible == false && _wasVisible)
        {
            onStopRendering?.Invoke();
        }

        UpdateWasVisibleVariable();
    }

    private bool IsVisible()
    {
        return (_transform.position - _camera.transform.position).sqrMagnitude < _renderDistance * _renderDistance;
    }

    private void UpdateIsVisibleVariable()
    {
        _isVisible = IsVisible();
    }

    private void UpdateWasVisibleVariable()
    {
        _wasVisible = _isVisible;
    }

    private void CheckSpawnedInvisibly()
    {
        UpdateIsVisibleVariable();

        if (_isVisible == false)
        {
            onStopRendering?.Invoke();
        }
    }

    #if UNITY_EDITOR

    [Button("Calculate Render Distance")]
    private void CalculateRenderDistance()
    {
        if (_transform == null)
        {
            Debug.LogError("Transform field is null");

            return;
        }

        _renderDistance = Vector3.Distance(UnityEditor.SceneView.lastActiveSceneView.camera.transform.position, _transform.position);
    }

    #endif
}
