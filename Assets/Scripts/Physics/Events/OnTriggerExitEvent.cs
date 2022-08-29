using System;
using UnityEngine;

public class OnTriggerExitEvent : MonoBehaviour
{
    [Header("Preferences")]
    [SerializeField] private LayerMask _layerMask;

    public Action<Collider> onExit;
    
    #region MonoBehaviour

    private void OnTriggerExit(Collider other)
    {
        if (_layerMask.ContainsLayer(other.gameObject.layer))
        {
            onExit?.Invoke(other);
        }
    }
    
    #endregion
}
