using TMPro;
using UnityEngine;

public class AdaptedTMPForColor : ColorAdapter
{
    [Header("References")]
    [SerializeField] private TMP_Text _tmpText;

    public override Color color
    {
        get => _tmpText.color;
        set => _tmpText.color = value;
    }
}
