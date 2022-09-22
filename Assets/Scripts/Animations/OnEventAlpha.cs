using UnityEngine;

public class OnEventAlpha : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AlphaAdapter _alphaAdapter;
    [SerializeField] private MonoEvent _monoEvent;

    [Header("Preferences")]
    [SerializeField, Range(0f, 1f)] private float _alpha;

    #region MonoBehaviour

    private void OnValidate()
    {
        _alphaAdapter ??= GetComponent<AlphaAdapter>();
        _monoEvent ??= GetComponent<MonoEvent>();
    }

    private void Awake()
    {
        _monoEvent.onMonoCall += SetAlpha;
    }

    private void OnDestroy()
    {
        _monoEvent.onMonoCall -= SetAlpha;
    }

    #endregion

    private void SetAlpha()
    {
        _alphaAdapter.alpha = _alpha;
    }
}
