using System;
using UnityEngine;
using UnityEngine.UI;

public class TargetFramerateSlider : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Slider _slider;
    [SerializeField] private GameFramerate _gameFramerate;

    #region MonoBehaviour

    private void OnValidate()
    {
        _slider ??= GetComponent<Slider>();
    }

    private void Awake()
    {
        _slider.maxValue = Screen.currentResolution.refreshRate;
    }

    private void Start()
    {
        _slider.value = Application.targetFrameRate;
    }

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(SetFramerate);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(SetFramerate);
    }

    #endregion

    private void SetFramerate(float framerate)
    {
        _gameFramerate.Set((int)framerate);
    }
}
