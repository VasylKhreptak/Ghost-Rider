using UnityEngine;
public class VolumeSliderValueOutput : SliderValueOutput
{
    [SerializeField] private string _endSign = "%";
    
    protected override void UpdateValue(float value)
    {
        _tmpText.text = _slider.value.ToString(_format) + _endSign;
    }
}
