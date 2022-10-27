using UnityEngine;

public class MeshTriangleNormalLinker : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _transform;
    [SerializeField] private InstancedMeshTriangleNormalsProvider _normalProvider;

    [Header("Preferences")]
    [SerializeField] private int _triangleIndex;

    private Vector3 _previousNormalDirection;

    #region MonoBehaviour

    private void OnValidate()
    {
        _transform ??= GetComponent<Transform>();
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
}
