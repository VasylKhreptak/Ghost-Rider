using UnityEngine;
using UnityEngine.UI;

public class CameraClippingSlider : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera _camera;
    [SerializeField] private Slider _slider;
    
    #region MonoBehaviour

    private void OnValidate()
    {
        _slider ??= GetComponent<Slider>();
        _camera = Camera.main;
    }

    private void Awake()
    {
        _slider.SetValueWithoutNotify(_camera.farClipPlane);
    }

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(SetClippingDistance);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(SetClippingDistance);
    }

    #endregion

    private void SetClippingDistance(float value)
    {
        _camera.farClipPlane = value;
    }
}
