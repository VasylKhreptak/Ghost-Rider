using UnityEngine;

public class TransformPositionLinker : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected Transform _transform;
    [SerializeField] private Transform _linkTo;

    [Header("Preferences")]
    [SerializeField] private Vector3 _linkAxis;
    [SerializeField] protected Vector3 _offset;

    #region MonoBehaviour

    private void OnValidate()
    {
        _transform ??= GetComponent<Transform>();
    }

    private void Update()
    {
        _transform.position = Vector3.Scale(_linkTo.position, _linkAxis) + _offset;
    }

    #endregion
}
