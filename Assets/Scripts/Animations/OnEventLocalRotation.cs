using UnityEngine;

public class OnEventLocalRotation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _transform;
    [SerializeField] private MonoEvent _monoEvent;

    [Header("Preferences")]
    [SerializeField] private Vector3 _rotation;

    #region MonoBehaviour

    private void OnValidate()
    {
        _transform = GetComponent<Transform>();
        _monoEvent = GetComponent<MonoEvent>();
    }

    private void Awake()
    {
        _monoEvent.onMonoCall += SetRotation;
    }

    private void OnDestroy()
    {
        _monoEvent.onMonoCall -= SetRotation;
    }

    #endregion

    private void SetRotation()
    {
        _transform.localRotation = Quaternion.Euler(_rotation);
    }
}
