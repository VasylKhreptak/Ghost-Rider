using UnityEngine;

public class ProbableMonoEvent : MonoEvent
{
    [Header("Preferences")]
    [SerializeField, Range(0f, 1f)] private float _probability;

    [Header("MonoEvent")]
    [SerializeField] private MonoEvent _monoEvent;

    #region MonoBehaviour

    private void OnValidate()
    {
        _monoEvent ??= GetComponent<MonoEvent>();
    }

    private void Awake()
    {
        _monoEvent.onMonoCall += InvokeWithProbability;
    }

    private void OnDestroy()
    {
        _monoEvent.onMonoCall -= InvokeWithProbability;
    }

    #endregion

    private void InvokeWithProbability()
    {
        Extensions.Mathf.Probability(_probability, () => { onMonoCall?.Invoke(); });
    }
}
