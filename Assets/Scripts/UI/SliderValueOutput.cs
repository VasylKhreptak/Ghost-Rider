using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueOutput : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected Slider _slider;
    [SerializeField] protected TMP_Text _tmpText;

    [Header("Preferences")]
    [SerializeField] protected string _format;
    
    #region MonoBehaviour

    private void OnValidate()
    {
        _slider ??= GetComponent<Slider>();
        _tmpText ??= GetComponent<TMP_Text>();
    }

    private void Start()
    {
        UpdateValue(_slider.value);
    }

    private void OnEnable()
    {
       _slider.onValueChanged.AddListener(UpdateValue);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(UpdateValue);
    }

    #endregion

    protected virtual void UpdateValue(float value)
    {
        _tmpText.text = _slider.value.ToString(_format);
    }
}
