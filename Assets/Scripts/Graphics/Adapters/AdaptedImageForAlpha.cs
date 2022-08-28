
using UnityEngine;
using UnityEngine.UI;

public class AdaptedImageForAlpha : AlphaAdapter
{
    [Header("References")]
    [SerializeField] private Image _image;

    #region MonoBehaviour

    private void OnValidate()
    {
        _image = GetComponent<Image>();
    }

    #endregion

    public override float alpha
    {
        get => _image.color.a;
        set => _image.color = _image.color.WithAlpha(value);
    }
}
