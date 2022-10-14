using System;
using UnityEngine;

public class RoadCarTailgateSpeedLimiter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private OnTriggerEnterEvent _triggerEnterEvent;
    [SerializeField] private OnTriggerExitEvent _triggerExitEvent;
    [SerializeField] private RCC_AICarController _aiCarController;
    [SerializeField] private RoadCarSpeedSetter _carSpeedSetter;
    [SerializeField] private RoadCarRandomSpeedSetter _carRandomSpeedSetter;

    [Header("Preferences")]
    [SerializeField] private LayerMask _mainCarLayerMask;

    private float _rawSpeed;

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

        _carSpeedSetter.SetSpeed(speed);
    }

    private void StopTailgate(Collider collider) => StopTailgate();

    private void StopTailgate()
    {
        _carSpeedSetter.SetSpeed(_rawSpeed);
    }
}
