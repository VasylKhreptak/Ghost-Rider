using System;
using TMPro;
using UnityEngine;

public class AdaptedTMPForColor : ColorAdapter
{
    [Header("References")]
    [SerializeField] private TMP_Text _tmpText;

    #region MonoBehaviour

    private void OnValidate()
    {
        _tmpText ??= GetComponent<TMP_Text>();
    }

    #endregion

    public override Color color
    {
        get => _tmpText.color;
        set => _tmpText.color = value;
    }
}
