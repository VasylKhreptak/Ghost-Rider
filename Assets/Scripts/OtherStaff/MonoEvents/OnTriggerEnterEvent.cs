using System;
using UnityEngine;

public class OnTriggerEnterEvent : MonoBehaviour
{
    [Header("Preferences")]
    [SerializeField] private LayerMask _layerMask;

    public Action<Collider> onEnter;
    
    #region MonoBehaviour

    private void OnTriggerEnter(Collider other)
    {
        if (_layerMask.ContainsLayer(other.gameObject.layer))
        {
            onEnter?.Invoke(other);
        }
    }
    
    #endregion
}
