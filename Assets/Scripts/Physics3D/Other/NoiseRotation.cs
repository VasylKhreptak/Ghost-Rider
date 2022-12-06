using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class NoiseRotation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _transform;

    [Header("Preferences")]
    [SerializeField] private Vector3 _amplitude;
    [SerializeField] private Vector3 _frequency;

    private Quaternion _startRotation;

    private CancellationTokenSource _cancellationTokenSource;
    
    private Stopwatch _stopwatch;

    private Vector3 _transformRight;
    private Vector3 _transformUp;
    private Vector3 _transformForward;

    private Quaternion _calculatedRotation;

    private float TimeFromStart => (float)_stopwatch.Elapsed.TotalSeconds;
    
    #region MonoBehaviour

    private void OnValidate()
    {
        _transform ??= GetComponent<Transform>();
    }

    private void Awake()
    {
        _startRotation = _transform.rotation;

        _cancellationTokenSource = new CancellationTokenSource();
        
        _stopwatch = Stopwatch.StartNew();
    }

    private void OnEnable()
    {
        _cancellationTokenSource.Dispose();
        
        _cancellationTokenSource = new CancellationTokenSource();
    }

    private void Update()
    {
        if (_amplitude == Vector3.zero) return;
        
        CacheVariables();

        CancellationToken cancellationToken = _cancellationTokenSource.Token;

        Task<Quaternion> task = Task.Run(GetRotation, cancellationToken);

        //task.Wait(cancellationToken);

        _calculatedRotation = task.Result;
        
        UpdateRotation();
    }

    private void OnDisable()
    {
        _cancellationTokenSource.Cancel();
    }

    private void OnDestroy()
    {
        _stopwatch.Stop();
    }

    #endregion

    private void UpdateRotation()
    {
        _transform.rotation = _calculatedRotation;
    }
    
    private void CacheVariables()
    {
        _transformRight = _transform.right;
        _transformUp = _transform.up;
        _transformForward = _transform.forward;
    }

    private Quaternion GetRotation()
    {
        float dx = (float)NoiseS3D.Noise(TimeFromStart * _frequency.x, 0f, 0f) * _amplitude.x;
        float dy = (float)NoiseS3D.Noise(0f, TimeFromStart * _frequency.y, 0f) * _amplitude.y;
        float dz = (float)NoiseS3D.Noise(0f, 0f, TimeFromStart * _frequency.z) * _amplitude.z;
        
        Quaternion rotation = _startRotation;
        
        rotation = Quaternion.AngleAxis(dx, _transformRight) * rotation;
        rotation = Quaternion.AngleAxis(dy, _transformUp) * rotation;
        rotation = Quaternion.AngleAxis(dz, _transformForward) * rotation;

        return rotation;
    }
}
