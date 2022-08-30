using System;
using UnityEngine;

public class OnCollisionExit2DEvent : MonoBehaviour
{
    [Header("Preferences")]
    [SerializeField] private LayerMask _layerMask;

    public Action<Collision2D> onExit;
    
    #region MonoBehaviour

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (_layerMask.ContainsLayer(collision.gameObject.layer))
        {
            onExit?.Invoke(collision);
        }
    }
    
    #endregion
}
