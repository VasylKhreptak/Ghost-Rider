using System;
using UnityEngine;

public class OnTriggerStayEvent : MonoBehaviour
{
    [Header("Preferences")]
    [SerializeField] private LayerMask _layerMask;

    public Action<Collider> onStay;
    
    #region MonoBehaviour

    private void OnTriggerStay(Collider other)
    {
        if (_layerMask.ContainsLayer(other.gameObject.layer))
        {
            onStay?.Invoke(other);
        }
    }
    
    #endregion
}
