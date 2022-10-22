using System;
using UnityEngine;

public class MeshDeformation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _transform;
    [SerializeField] private OnCollisionEnterEvent _collisionEnterEvent;
    [SerializeField] private MeshFilter _meshFilter;
    [SerializeField] private MeshCollider _meshCollider;

    [Header("Preferences")]
    [SerializeField] private bool _updateCollider;
    [SerializeField] private float _deformRadius;
    [SerializeField] private float _maxVertexDeform;
    [SerializeField] private float _minImpulse;
    [SerializeField] private float _damageMultiplier;
    [SerializeField] private AnimationCurve _damageSpreadCurve;

    private Vector3[] _meshVertices;
    private Vector3[] _startMeshVertices;

    public Action onDeform;

    #region MonoBehaviour

    private void Awake()
    {
        Mesh mesh = _meshFilter.mesh;
        _meshVertices = mesh.vertices;
        _startMeshVertices = mesh.vertices;
    }

    private void OnValidate()
    {
        _transform ??= GetComponent<Transform>();
        _collisionEnterEvent ??= GetComponent<OnCollisionEnterEvent>();
        _meshFilter ??= GetComponent<MeshFilter>();
        _meshCollider ??= GetComponent<MeshCollider>();
    }

    private void OnEnable()
    {
        _collisionEnterEvent.onEnter += DeformMesh;
    }

    private void OnDisable()
    {
        _collisionEnterEvent.onEnter -= DeformMesh;
    }

    #endregion

    private void DeformMesh(Collision collision)
    {
        float collisionImpulse = collision.impulse.magnitude;

        if (collisionImpulse < _minImpulse) return;

        foreach (var contactPoint in collision.contacts)
        {
            for (int i = 0; i < _meshVertices.Length; i++)
            {
                Vector3 vertexPosition = _meshVertices[i];
                Vector3 pointPosition = _transform.InverseTransformPoint(contactPoint.point);

                float distanceFromCollision = Vector3.Distance(vertexPosition, pointPosition);
                float distanceFromOriginalVertex = Vector3.Distance(_startMeshVertices[i], vertexPosition);

                if (distanceFromCollision < _deformRadius && distanceFromOriginalVertex < _maxVertexDeform)
                {
                    float falloff = _damageSpreadCurve.Evaluate(distanceFromCollision / _deformRadius);

                    float deformedX = Mathf.Clamp(pointPosition.x * falloff, 0, _maxVertexDeform);
                    float deformedY = Mathf.Clamp(pointPosition.y * falloff, 0, _maxVertexDeform);
                    float deformedZ = Mathf.Clamp(pointPosition.z * falloff, 0, _maxVertexDeform);

                    Vector3 deformation = new Vector3(deformedX, deformedY, deformedZ);
                    _meshVertices[i] -= deformation * _damageMultiplier;
                }
            }
        }

        onDeform?.Invoke();

        UpdateModel();
    }

    private void UpdateModel()
    {
        _meshFilter.mesh.vertices = _meshVertices;

        if (_updateCollider)
        {
            UpdateCollider();
        }
    }

    private void UpdateCollider()
    {
        _meshCollider.sharedMesh = _meshFilter.mesh;
    }

    public void RestoreMesh()
    {
        _meshFilter.mesh.vertices = _startMeshVertices;
        
        RestoreVertices();
    }

    public void RestoreCollider()
    {
        _meshCollider.sharedMesh = _meshFilter.mesh;
    }

    public void RestoreVertices()
    {
        _meshVertices = _meshFilter.mesh.vertices;
    }
}
