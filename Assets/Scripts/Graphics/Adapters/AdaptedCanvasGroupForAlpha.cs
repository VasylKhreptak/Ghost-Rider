using UnityEngine;

public class AdaptedCanvasGroupForAlpha : AlphaAdapter
{
    [Header("References")]
    [SerializeField] private CanvasGroup _canvasGroup;

    #region MonoBehaviour

    private void OnValidate()
    {
        _canvasGroup ??= GetComponent<CanvasGroup>();
    }

    #endregion

    public override float alpha
    {
        get => _canvasGroup.alpha;
        set => _canvasGroup.alpha = value;
    }
}
