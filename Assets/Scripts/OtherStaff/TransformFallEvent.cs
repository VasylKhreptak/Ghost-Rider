using UnityEngine;

public class TransformFallEvent : MonoEvent
{
    [Header("References")]
    [SerializeField] private Transform _transform;

    [Header("Preferences")]
    [SerializeField] private float _fallTriggerY = -3;
    [SerializeField] private float _checkDelay = 1f;

    private ConfigurableUpdate _configurableUpdate = new ConfigurableUpdate();

    #region MonoBehaviour

    private void Awake()
    {
        _configurableUpdate.Init(this, _checkDelay, Check);
    }

    private void OnValidate()
    {
        _transform ??= GetComponent<Transform>();
    }

    private void OnEnable()
    {
        _configurableUpdate.StartUpdating();
    }

    private void OnDisable()
    {
        _configurableUpdate.StopUpdating();
    }

    #endregion

    private void Check()
    {
        if (_transform.position.y < _fallTriggerY)
        {
            onMonoCall?.Invoke();
        }
    }
}
