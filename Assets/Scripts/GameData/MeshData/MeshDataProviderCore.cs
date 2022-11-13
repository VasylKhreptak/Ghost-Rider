using System;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshDataProviderCore : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected Transform _transform;
    [SerializeField] protected MeshFilter _meshFilter;

    public Transform Transform => _transform;
    
    public Action onLoad;
    public Action onUpdate;
    
    #region MonoBehaviour

    private void OnValidate()
    {
        _transform ??= GetComponent<Transform>();
        _meshFilter ??= GetComponent<MeshFilter>();
    }

    private void Awake()
    {
        SyncData();
    }

    private void Start()
    {
        onLoad?.Invoke();
    }

    #endregion

    public void UpdateData()
    {
        SyncData();

        onUpdate?.Invoke();
    }

    protected virtual void SyncData()
    {
        throw new NotImplementedException();
    }
}
