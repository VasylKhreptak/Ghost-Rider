using UnityEngine;

public class OnEventColor : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ColorAdapter _colorAdapter;
    [SerializeField] private MonoEvent _monoEvent;

    [Header("Preferences")]
    [SerializeField] private Color _color;

    #region MonoBehaviour

    private void OnValidate()
    {
        _colorAdapter ??= GetComponent<ColorAdapter>();
        _monoEvent ??= GetComponent<MonoEvent>();
    }

    private void Awake()
    {
        _monoEvent.onMonoCall += SetColor;
    }

    private void OnDestroy()
    {
        _monoEvent.onMonoCall -= SetColor;
    }

    #endregion

    private void SetColor()
    {
        _colorAdapter.color = _color;
    }
}
