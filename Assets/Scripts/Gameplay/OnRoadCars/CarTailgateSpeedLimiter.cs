using System;
using DG.Tweening;
using UnityEngine;

public class CarTailgateSpeedLimiter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private OnTriggerEnterEvent _triggerEnterEvent;
    [SerializeField] private OnTriggerExitEvent _triggerExitEvent;
    [SerializeField] private RCC_AICarController _aiCarController;
    [SerializeField] private RoadCarSpeedSetter _carSpeedSetter;
    [SerializeField] private RoadCarRandomSpeedSetter _carRandomSpeedSetter;

    [Header("Preferences")]
    [SerializeField] private LayerMask _mainCarLayerMask;
    [SerializeField] private float _speedChangeDuration;
    [SerializeField] private AnimationCurve _speedChangeCurve;

    private float _rawSpeed;

    private Tween _speedChangeTween;
    
    #region MonoBehaviour

    private void OnValidate()
    {
        _triggerEnterEvent ??= GetComponent<OnTriggerEnterEvent>();
        _triggerExitEvent ??= GetComponent<OnTriggerExitEvent>();
        _aiCarController ??= transform.parent.GetComponent<RCC_AICarController>();
        _carSpeedSetter ??= transform.parent.GetComponent<RoadCarSpeedSetter>();
        _carRandomSpeedSetter ??= transform.parent.GetComponent<RoadCarRandomSpeedSetter>();
    }

    private void Awake()
    {
        _rawSpeed = _aiCarController.maximumSpeed;
    }
    private void OnEnable()
    {
        _triggerEnterEvent.onEnter += StartTailgate;
        _triggerExitEvent.onExit += StopTailgate;
        _carRandomSpeedSetter.onSet += UpdateRawSpeed;
    }

    private void OnDisable()
    {
        _triggerEnterEvent.onEnter -= StartTailgate;
        _triggerExitEvent.onExit -= StopTailgate;
        _carRandomSpeedSetter.onSet -= UpdateRawSpeed;

        _speedChangeTween.Kill();
    }

    #endregion

    private void UpdateRawSpeed(float speed)
    {
        _rawSpeed = speed;
    }

    private void StartTailgate(Collider collider)
    {
        float speed = 0;

        GameObject frontCarObject = collider.transform.parent.parent.gameObject;
        
        if (_mainCarLayerMask.ContainsLayer(frontCarObject.layer))
        {
            speed = Math.Clamp(frontCarObject.GetComponent<RCC_CarControllerV3>().speed, 0, _rawSpeed);
        }
        else
        {
            speed = frontCarObject.GetComponent<RCC_AICarController>().maximumSpeed;
        }

        SetSpeedSmoothly(speed);
    }

    private void StopTailgate(Collider collider) => StopTailgate();

    private void StopTailgate()
    {
        SetSpeedSmoothly(_rawSpeed);
    }

    private void SetSpeedSmoothly(float speed)
    {
        _speedChangeTween.Kill();

        _speedChangeTween = DOTween
            .To(() => _aiCarController.maximumSpeed, x => _carSpeedSetter.SetSpeed(x), speed, _speedChangeDuration)
            .SetEase(_speedChangeCurve);
    }
}
