using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class NoiseMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _transform;

    [Header("Preferences")]
    [SerializeField] private Vector3 _amplitude;
    [SerializeField] private Vector3 _frequency;

    private Vector3 _startPosition;

    private CancellationTokenSource _cancellationTokenSource;

    private Stopwatch _stopwatch;
    
    private Vector3 _calculatedPosition;
    
    private Vector3 _transformRight;
    private Vector3 _transformUp;
    private Vector3 _transformForward;
    
    private float TimeFromStart => (float)_stopwatch.Elapsed.TotalSeconds;

    #region MonoBehaviour

    private void OnValidate()
    {
        _transform ??= GetComponent<Transform>();
    }

    private void Awake()
    {
        _startPosition = _transform.position;

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

        Task<Vector3> task = Task.Run(GetPosition, cancellationToken);

        task.Wait(cancellationToken);

        _calculatedPosition = task.Result;
        
        UpdatePosition();
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

    private void UpdatePosition()
    {
        _transform.position = _calculatedPosition;
    }

    private void CacheVariables()
    {
        _transformRight = _transform.right;
        _transformUp = _transform.up;
        _transformForward = _transform.forward;
    }
    
    private Vector3 GetPosition()
    {
        float dx = (float)NoiseS3D.Noise(TimeFromStart * _frequency.x, 0f, 0f) * _amplitude.x;
        float dy = (float)NoiseS3D.Noise(0f, TimeFromStart * _frequency.y, 0f) * _amplitude.y;
        float dz = (float)NoiseS3D.Noise(0f, 0f, TimeFromStart * _frequency.z) * _amplitude.z;

        Vector3 position = _startPosition;

        position += _transformRight * dx;
        position += _transformUp * dy;
        position += _transformForward * dz;

        return position;
    }
}
