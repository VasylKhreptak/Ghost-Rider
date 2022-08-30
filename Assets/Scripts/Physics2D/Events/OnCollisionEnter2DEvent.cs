using System;
using UnityEngine;

public class OnCollisionEnter2DEvent : MonoBehaviour
{
    [Header("Preferences")]
    [SerializeField] private LayerMask _layerMask;

    public Action<Collision2D> onEnter;
    
    #region MonoBehaviour

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_layerMask.ContainsLayer(collision.gameObject.layer))
        {
            onEnter?.Invoke(collision);
        }
    }
    
    #endregion
}
