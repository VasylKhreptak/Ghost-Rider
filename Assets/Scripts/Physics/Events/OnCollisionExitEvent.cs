using System;
using UnityEngine;

public class OnCollisionExitEvent : MonoBehaviour
{
    [Header("Preferences")]
    [SerializeField] private LayerMask _layerMask;

    public Action<Collision> onExit;
    
    #region MonoBehaviour

    private void OnCollisionExit(Collision collision)
    {
        if (_layerMask.ContainsLayer(collision.gameObject.layer))
        {
            onExit?.Invoke(collision);
        }
    }
    
    #endregion
}
