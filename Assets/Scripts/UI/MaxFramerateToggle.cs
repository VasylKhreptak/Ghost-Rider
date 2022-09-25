using UnityEngine;
using UnityEngine.UI;

public class MaxFramerateToggle : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Toggle _toggle;
    [SerializeField] private GameFramerate _gameFramerate;

    #region MonoBehaciour

    private void OnValidate()
    {
        _toggle ??= GetComponent<Toggle>();
    }

    private void OnEnable()
    {
        _toggle.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveListener(OnValueChanged);
    }

    #endregion

    private void OnValueChanged(bool state)
    {
        if (state)
        {
            _gameFramerate.Set(int.MaxValue);
        }
    }
}