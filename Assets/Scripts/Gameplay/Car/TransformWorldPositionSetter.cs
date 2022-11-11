using System;
using UnityEngine;

public class TransformWorldPositionSetter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _transform;

    [Header("Preferences")]
    [SerializeField] private Vector3 _position;

    #region MonoBehaviour

    private void OnValidate()
    {
        _transform ??= GetComponent<Transform>();
    }

    #endregion
    
    public void SetPosition()
    {
        _transform.position = _position;
    }
}
