using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TMP_SmoothIntText : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text _tmp;

    [Header("Preferences")]
    [SerializeField] private float _duration;
    [SerializeField] private AnimationCurve _curve;

    private Tween _tween;

    public string text
    {
        get => _tmp.text;
        set
        {
            if (int.TryParse(value, out int result) == false)
            {
                _tmp.text = "Invalid parameter!";
                throw new ArgumentException();
            }

            SetSmooth(result);
        }
    }

    #region MonoBehaviour

    private void OnValidate()
    {
        _tmp ??= GetComponent<TMP_Text>();
    }

    private void OnDisable()
    {
        _tween.Kill();
    }

    #endregion

    private void SetSmooth(int value)
    {
        _tween.Kill();

        _tween = DOTween
            .To(() => int.Parse(_tmp.text), x => _tmp.text = x.ToString(), value, _duration)
            .SetEase(_curve)
            .OnKill(() => _tmp.text = value.ToString());
    }
}
