using UnityEngine;

public class OnEventAnchorPosition : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private MonoEvent _monoEvent;

    [Header("Preferences")]
    [SerializeField] private Vector2 _anchorPosition;

    #region MonoBehaviour

    private void OnValidate()
    {
        _rectTransform = GetComponent<RectTransform>();
        _monoEvent = GetComponent<MonoEvent>();
    }

    private void Awake()
    {
        _monoEvent.onMonoCall += SetAnchoredPosition;
    }

    private void OnDestroy()
    {
        _monoEvent.onMonoCall -= SetAnchoredPosition;
    }

    #endregion

    private void SetAnchoredPosition()
    {
        _rectTransform.anchoredPosition = _anchorPosition;
    }
}
