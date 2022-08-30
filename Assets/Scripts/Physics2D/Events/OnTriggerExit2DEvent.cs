using System;
using UnityEngine;

public class OnTriggerExit2DEvent : MonoBehaviour
{
    [Header("Preferences")]
    [SerializeField] private LayerMask _layerMask;

    public Action<Collider2D> onExit;
    
    #region MonoBehaviour

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_layerMask.ContainsLayer(other.gameObject.layer))
        {
            onExit?.Invoke(other);
        }
    }
    
    #endregion
}
