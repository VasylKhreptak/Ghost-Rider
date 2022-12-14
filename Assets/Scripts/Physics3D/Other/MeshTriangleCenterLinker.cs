using UnityEngine;

public class MeshTriangleCenterLinker : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _transform;
    [SerializeField] private InstancedMeshTriangleCentersProvider _centersProvider;

    [Header("Preferences")]
    [SerializeField] private int _triangleIndex;

    [Header("Events")]
    [SerializeField] private MonoEvent _restoreEvent;

    private Vector3 _previousCenterPosition;
    private Vector3 _startCenterPosition;
    private Vector3 _startLocalPosition;

    #region MonoBehaviour

    private void OnValidate()
    {
        _transform ??= GetComponent<Transform>();
        _restoreEvent ??= GetComponent<MonoEvent>();
    }
    private void Awake()
    {
        _startLocalPosition = _transform.localPosition;
        
        _centersProvider.onLoad += OnLoad;
        _restoreEvent.onMonoCall += Restore;
    }

    private void OnEnable()
    {
        _centersProvider.onUpdate += UpdatePosition;
    }

    private void OnDisable()
    {
        _centersProvider.onUpdate -= UpdatePosition;
    }

    private void OnDestroy()
    {
        _centersProvider.onLoad -= OnLoad;
        _restoreEvent.onMonoCall -= Restore;
    }

    #endregion

    private void OnLoad()
    {
        UpdateStartCenterPosition();

        UpdatePreviousCenterPosition();
    }
    
    private void UpdatePosition()
    {
        Vector3 center = _centersProvider.centers[_triangleIndex];

        if (_previousCenterPosition == Vector3.zero)
        {
            Debug.Log("Zero");
        }
        
        if (center == _previousCenterPosition) return;

        Vector3 direction = (center - _previousCenterPosition).normalized;
        float distance = Vector3.Distance(center, _previousCenterPosition);
        
        _transform.localPosition += (direction * distance);
        
        UpdatePreviousCenterPosition();
    }

    private void UpdatePreviousCenterPosition()
    {
        _previousCenterPosition = _centersProvider.centers[_triangleIndex];
    }

    private void UpdateStartCenterPosition()
    {
        _startCenterPosition = _centersProvider.centers[_triangleIndex];
    }

    private void Restore()
    {
        _transform.localPosition = _startLocalPosition;
        _previousCenterPosition = _startCenterPosition;
    }
}
