using UnityEngine;

public class OnEventLocalPosition : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _transform;
    [SerializeField] private MonoEvent _monoEvent;

    [Header("Preferences")]
    [SerializeField] private Vector3 _localPosition;

    #region MonoBehaviour

    private void OnValidate()
    {
        _transform = GetComponent<Transform>();
        _monoEvent = GetComponent<MonoEvent>();
    }

    private void Awake()
    {
        _monoEvent.onMonoCall += SetLocalPosition;
    }

    private void OnDestroy()
    {
        _monoEvent.onMonoCall -= SetLocalPosition;
    }

    #endregion

    private void SetLocalPosition()
    {
        _transform.localPosition = _localPosition;
    }
}
