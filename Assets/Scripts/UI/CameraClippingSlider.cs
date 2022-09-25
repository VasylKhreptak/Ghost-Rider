using System;
using UnityEngine;
using UnityEngine.UI;

public class CameraClippingSlider : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Slider _slider;

    private Camera _camera;
    
    #region MonoBehaviour

    private void OnValidate()
    {
        _slider ??= GetComponent<Slider>();
    }

    private void Awake()
    {
        _camera = Camera.main;
        
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
