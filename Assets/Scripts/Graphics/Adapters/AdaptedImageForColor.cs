using System;
using UnityEngine;
using UnityEngine.UI;

public class AdaptedImageForColor : ColorAdapter
{
    [Header("References")]
    [SerializeField] private Image _image;

    #region MonoBehaviour

    private void OnValidate()
    {
        _image ??= GetComponent<Image>();
    }

    #endregion

    public override Color color
    {
        get => _image.color;
        set => _image.color = value;
    }
}