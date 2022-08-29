using System;
using UnityEngine;

public class OnCollisionStayEvent : MonoBehaviour
{
    [Header("Preferences")]
    [SerializeField] private LayerMask _layerMask;

    public Action<Collision> onStay;
    
    #region MonoBehaviour

    private void OnCollisionStay(Collision collision)
    {
        if (_layerMask.ContainsLayer(collision.gameObject.layer))
        {
            onStay?.Invoke(collision);
        }
    }
    
    #endregion
}
