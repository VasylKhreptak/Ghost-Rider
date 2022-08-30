using System;
using UnityEngine;

public class OnTriggerEnter2DEvent : MonoBehaviour
{
    [Header("Preferences")]
    [SerializeField] private LayerMask _layerMask;

    public Action<Collider2D> onEnter;
    
    #region MonoBehaviour

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_layerMask.ContainsLayer(other.gameObject.layer))
        {
            onEnter?.Invoke(other);
        }
    }
    
    #endregion
}
