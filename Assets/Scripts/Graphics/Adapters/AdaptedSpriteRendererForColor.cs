using System;
using UnityEngine;

public class AdaptedSpriteRendererForColor : ColorAdapter
{
    [Header("References")]
    [SerializeField] private SpriteRenderer _spriteRenderer;

    #region MonoBehaviour

    private void OnValidate()
    {
        _spriteRenderer ??= GetComponent<SpriteRenderer>();
    }

    #endregion
    
    public override Color color
    {
        get => _spriteRenderer.color;
        set => _spriteRenderer.color = value;
    }
}
