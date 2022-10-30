using System;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshDataProviderCore : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected Transform _transform;
    [SerializeField] protected MeshFilter _meshFilter;
    
    public Action onLoad;
    public Action onUpdate;
    
    #region MonoBehaviour

    private void OnValidate()
    {
        _transform ??= GetComponent<Transform>();
        _meshFilter ??= GetComponent<MeshFilter>();
    }

    private void Start()
    {
        LoadData();
    }

    #endregion

    private void LoadData()
    {
        SyncData();

        onLoad?.Invoke();
    }
    
    public void UpdateData()
    {
        SyncData();

        onUpdate?.Invoke();
    }

    protected virtual void SyncData()
    {
        throw new NotImplementedException();
    }

    protected virtual void PreloadData()
    {
        throw new NotImplementedException();
    }
}
