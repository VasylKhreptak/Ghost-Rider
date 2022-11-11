using UnityEngine;

public class OnEventWorldPosition : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _transform;

    [Header("Preferences")]
    [SerializeField] private Vector3 _position;

    [Header("Events")]
    [SerializeField] private MonoEvent _event;

    #region MonoBehaviour

    private void OnValidate()
    {
        _transform ??= GetComponent<Transform>();
        _event ??= GetComponent<MonoEvent>();
    }

    private void Awake()
    {
        _event.onMonoCall += SetPosition;
    }

    private void OnDestroy()
    {
        _event.onMonoCall -= SetPosition;
    }

    #endregion

    private void SetPosition()
    {
        _transform.position = _position;
    }
}
