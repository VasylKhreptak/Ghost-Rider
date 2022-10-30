using UnityEngine;

public class MeshTriangleNormalLinker : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _transform;
    [SerializeField] private InstancedMeshTriangleNormalsProvider _normalProvider;
    [SerializeField] private SharedMeshTriangleNormalsProvider _startNormalsProvider;

    [Header("Preferences")]
    [SerializeField] private int _triangleIndex;

    [Header("Events")]
    [SerializeField] private MonoEvent _restoreEvent;
    
    private Vector3 _previousNormalDirection;
    private Vector3 _startLocalRotation;
    
    #region MonoBehaviour

    private void OnValidate()
    {
        _transform ??= GetComponent<Transform>();
        _restoreEvent ??= GetComponent<MonoEvent>();
    }

    private void Awake()
    {
        _startLocalRotation = _transform.localRotation.eulerAngles;

        _restoreEvent.onMonoCall += Restore;
    }

    private void Start()
    {
        UpdateLookDirection();
    }

    private void OnEnable()
    {
        _normalProvider.onUpdate += UpdateLookDirection;
        _normalProvider.onLoad += UpdatePreviousNormalDirection;

    }

    private void OnDisable()
    {
        _normalProvider.onUpdate -= UpdateLookDirection;
        _normalProvider.onLoad -= UpdatePreviousNormalDirection;
    }

    private void OnDestroy()
    {
        _restoreEvent.onMonoCall -= Restore;
    }

    #endregion

    private void UpdateLookDirection()
    {
        Vector3 normal = _normalProvider.normals[_triangleIndex];
        Quaternion normalRotation = Quaternion.FromToRotation(_previousNormalDirection, normal);
        _transform.forward = normalRotation * _transform.forward;
        
        UpdatePreviousNormalDirection();
    }

    private void UpdatePreviousNormalDirection()
    {
        _previousNormalDirection = _normalProvider.normals[_triangleIndex];
    }

    private void Restore()
    {
        _transform.localRotation = Quaternion.Euler(_startLocalRotation);
        _previousNormalDirection = _startNormalsProvider.normals[_triangleIndex];
    }
}
