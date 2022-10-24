using System;
using UnityEngine;

public class MeshDeformation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _transform;
    [SerializeField] private OnCollisionEnterEvent _collisionEnterEvent;
    [SerializeField] private MeshFilter _meshFilter;
    [SerializeField] private MeshCollider _meshCollider;
    [SerializeField] private InstancedMeshVerticesProvider _verticesProvider;
    [SerializeField] private SharedMeshVerticesProvider _startVerticesProvider;

    [Header("Preferences")]
    [SerializeField] private bool _updateCollider;

    [Header("Deformation Preferences")]
    [SerializeField] private float _minDeformationRadius;
    [SerializeField] private float _maxDeformationRadius;
    [SerializeField] private AnimationCurve _radiusDeformationCurve;
    [SerializeField] private float _minVertexDeformation;
    [SerializeField] private float _maxVertexDeformation;
    [SerializeField] private AnimationCurve _deformationCurve;
    [SerializeField] private float _minImpulse;
    [SerializeField] private float _maxImpulse;
    [SerializeField] private AnimationCurve _falloffCurve;
    [SerializeField] private float _deformationMultiplier;

    public Action onDeform;

    #region MonoBehaviour

    private void OnValidate()
    {
        _transform ??= GetComponent<Transform>();
        _collisionEnterEvent ??= GetComponent<OnCollisionEnterEvent>();
        _meshFilter ??= GetComponent<MeshFilter>();
        _meshCollider ??= GetComponent<MeshCollider>();
        _verticesProvider ??= GetComponentInChildren<InstancedMeshVerticesProvider>();
        _startVerticesProvider ??= GetComponentInChildren<SharedMeshVerticesProvider>();
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

        float radius = GetDeformationRadius(ref collisionImpulse);

        float deformation = _deformationCurve.Evaluate(_minImpulse, _maxImpulse, collisionImpulse,
            _minVertexDeformation, _maxVertexDeformation) * _deformationMultiplier;
        
        foreach (var contactPoint in collision.contacts)
        {
            Vector3 deformationDirection = contactPoint.normal;
            
            for (int i = 0; i < _verticesProvider.vertices.Length; i++)
            {
                Vector3 vertexPosition = _verticesProvider.vertices[i];
                Vector3 pointPosition = _transform.InverseTransformPoint(contactPoint.point);

                float distanceFromCollision = Vector3.Distance(vertexPosition, pointPosition);
                float distanceFromOriginalVertex = Vector3.Distance(_startVerticesProvider.vertices[i], vertexPosition);
                
                if (distanceFromCollision < radius && distanceFromOriginalVertex < _maxVertexDeformation)
                {
                    _verticesProvider.vertices[i] = GetNewVertexPosition(ref deformation, _verticesProvider.vertices[i], 
                        ref deformationDirection, ref distanceFromCollision, ref radius);
                }
            }
        }
        
        onDeform?.Invoke();

        OnUpdate();
    }

    private void OnUpdate()
    {
        UpdateMesh();
        
        if (_updateCollider)
        {
            UpdateCollider();
        }
    }

    private void UpdateMesh()
    {
        _meshFilter.mesh.vertices = _verticesProvider.vertices;
    }
    
    private void UpdateCollider()
    {
        _meshCollider.sharedMesh = _meshFilter.mesh;
    }

    public void RestoreMesh()
    {
        _meshFilter.mesh.vertices = _startVerticesProvider.vertices;

        RestoreVertices();
    }

    public void RestoreCollider()
    {
        _meshCollider.sharedMesh = _meshFilter.mesh;
    }

    public void RestoreVertices()
    {
        _verticesProvider.vertices = _meshFilter.mesh.vertices;
    }

    private float GetDeformationRadius(ref float impulse)
    {
        return _radiusDeformationCurve
            .Evaluate(_minImpulse, _maxImpulse, impulse, _minDeformationRadius, _maxDeformationRadius);
    }

    private Vector3 GetNewVertexPosition(ref float deformation, Vector3 previousVertexPosition,
        ref Vector3 deformationDirection, ref float distanceFromCollision, ref float radius)
    {
        Vector3 previousWorldPosition = _transform.TransformPoint(previousVertexPosition);
        
        float falloff = _falloffCurve.Evaluate(distanceFromCollision / radius);

        Vector3 newWorldPosition = previousWorldPosition + (deformationDirection * deformation * falloff);

        return _transform.InverseTransformPoint(newWorldPosition);
    }
}
