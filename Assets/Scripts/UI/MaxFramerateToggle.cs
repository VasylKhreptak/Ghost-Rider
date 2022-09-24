using UnityEngine;
using UnityEngine.UI;

public class MaxFramerateToggle : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Toggle _toggle;
    [SerializeField] private Slider _targetFramerateSlider;
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
        _gameFramerate.Set(state ? int.MaxValue : (int)_targetFramerateSlider.value);

        _targetFramerateSlider.interactable = !state;
    }
}
