using System;
using UnityEngine;

public class OnCollisionStay2DEvent : MonoBehaviour
{
    [Header("Preferences")]
    [SerializeField] private LayerMask _layerMask;

    public Action<Collision2D> onStay;
    
    #region MonoBehaviour

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (_layerMask.ContainsLayer(collision.gameObject.layer))
        {
            onStay?.Invoke(collision);
        }
    }
    
    #endregion
}
