using UnityEngine;

public class AdaptedSpriteRendererForAlpha : AlphaAdapter
{
    [Header("References")]
    [SerializeField] private SpriteRenderer _spriteRenderer;

    #region MonoBehaviour

    private void OnValidate()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    #endregion

    public override float alpha
    {
        get => _spriteRenderer.color.a;
        set => _spriteRenderer.color = _spriteRenderer.color.WithAlpha(value);
    }
}
