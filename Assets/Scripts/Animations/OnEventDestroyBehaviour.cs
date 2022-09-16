using UnityEngine;

public class OnEventDestroyBehaviour : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MonoBehaviour[] _behaviours;

    [Header("MonoEvent")]
    [SerializeField] private MonoEvent _event;

    #region MonoBehaviour

    private void OnValidate()
    {
        _event ??= GetComponent<MonoEvent>();
    }

    private void Awake()
    {
        _event.onMonoCall += DestroyBehaviours;
    }

    private void OnDestroy()
    {
        _event.onMonoCall -= DestroyBehaviours;
    }

    #endregion

    private void DestroyBehaviours()
    {
        foreach (var script in _behaviours)
        {
            Destroy(script);
        }
    }
}
