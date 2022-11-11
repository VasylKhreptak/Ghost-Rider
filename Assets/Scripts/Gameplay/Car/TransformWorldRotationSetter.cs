using System;
using UnityEngine;

public class TransformWorldRotationSetter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _transform;

    [Header("Preferences")]
    [SerializeField] private Vector3 _rotation;

    #region MonoBehaviour

    private void OnValidate()
    {
        _transform ??= GetComponent<Transform>();
    }

    #endregion

    public void SetRotation()
    {
        _transform.rotation = Quaternion.Euler(_rotation);
    }
}
