using System.Collections;
using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text _tmp;

    [Header("Preferences")]
    [SerializeField] private float _updateDelay;

    private Coroutine _countCoroutine;

    #region MonoBehaviour

    private void OnValidate()
    {
        _tmp ??= GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        StartCounting();
    }

    private void OnDisable()
    {
        StopCounting();
    }

    #endregion

    private void StartCounting()
    {
        if (_countCoroutine == null)
        {
            _countCoroutine = StartCoroutine(CountRoutine());
        }
    }

    private void StopCounting()
    {
        if (_countCoroutine != null)
        {
            StopCoroutine(_countCoroutine);

            _countCoroutine = null;
        }
    }

    private IEnumerator CountRoutine()
    {
        while (true)
        {
            UpdateText();
            
            yield return new WaitForSeconds(_updateDelay);
        }
    }

    private void UpdateText()
    {
        int framerate = (int)(1f / Time.unscaledDeltaTime);

        _tmp.text = framerate.ToString();
    }
}

