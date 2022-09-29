using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ConsoleLog : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text _tmpText;

    [Header("Preferences")]
    [SerializeField] private float _showTime = 3f;

    private Tween _waitTween;

    #region MonoBehaviour

    private void OnValidate()
    {
        _tmpText ??= GetComponent<TMP_Text>();
    }

    #endregion

    private void OnEnable()
    {
        Application.logMessageReceived += ProcessLog;
    }

    private void OnDisable()
    {
        Application.logMessageReceived -= ProcessLog;

        _waitTween.Kill();
    }

    private void ProcessLog(string log, string stackTrace, LogType logType)
    {
        _waitTween.Kill();
        _tmpText.text += "\n* " + log;
        _waitTween = this.DOWait(_showTime).OnComplete(() => { _tmpText.text = ""; });
    }
}
