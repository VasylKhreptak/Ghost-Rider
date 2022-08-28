using UnityEngine;

public class OnEventLocalScale : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private  Transform _transform;
    [SerializeField] private MonoEvent _monoEvent;

    [Header("Preferences")]
    [SerializeField] private Vector3 _scale;

    #region MonoBehaviour

    private void OnValidate()
    {
        _transform = GetComponent<Transform>();
        _monoEvent = GetComponent<MonoEvent>();
    }

    private void Awake()
    {
        _monoEvent.onMonoCall += SetLocalScale;
    }

    private void OnDestroy()
    {
        _monoEvent.onMonoCall -= SetLocalScale;
    }

    #endregion

    private void SetLocalScale()
    {
        _transform.localScale = _scale;
    }
}
