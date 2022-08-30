using System;
using UnityEngine;

public class OnTriggerStay2DEvent : MonoBehaviour
{
    [Header("Preferences")]
    [SerializeField] private LayerMask _layerMask;

    public Action<Collider2D> onStay;
    
    #region MonoBehaviour

    private void OnTriggerStay2D(Collider2D other)
    {
        if (_layerMask.ContainsLayer(other.gameObject.layer))
        {
            onStay?.Invoke(other);
        }
    }
    
    #endregion
}
