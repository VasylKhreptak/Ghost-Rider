using System;
using UnityEngine;

public class OnCollisionEnterEvent : MonoBehaviour
{
    [Header("Preferences")]
    [SerializeField] private LayerMask _layerMask;

    public Action<Collision> onEnter;
    
    #region MonoBehaviour

    private void OnCollisionEnter(Collision collision)
    {
        if (_layerMask.ContainsLayer(collision.gameObject.layer))
        {
            onEnter?.Invoke(collision);
        }
    }
    
    #endregion
}
