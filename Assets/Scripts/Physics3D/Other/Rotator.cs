using System;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _transform;

    [Header("Preferences")]
    [SerializeField] private Vector3 _angle;
    
    #region MonoBehaviour

    private void OnValidate()
    {
        _transform ??= GetComponent<Transform>();
    }

    private void Update()
    {
        _transform.Rotate(_angle * Time.deltaTime);
    }

    #endregion
}
