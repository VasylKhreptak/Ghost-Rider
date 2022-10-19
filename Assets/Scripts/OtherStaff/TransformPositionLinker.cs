using UnityEngine;
using UnityEngine.Serialization;

public class TransformPositionLinker : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _transform;
    [FormerlySerializedAs("_linkTo")] public Transform linkTo;
    
    [Header("Preferences")] 
    [FormerlySerializedAs("_linkAxis")] public Vector3 linkAxis;
    [FormerlySerializedAs("_offset")] [SerializeField] public Vector3 offset;

    #region MonoBehaviour

    private void OnValidate()
    {
        _transform ??= GetComponent<Transform>();
    }

    private void Update()
    {
        if (linkTo == null) return;
        
        _transform.position = Vector3.Scale(linkTo.position, linkAxis) + offset;
    }

    #endregion
}
