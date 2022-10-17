using System.Linq;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class CarTailgating : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _transform;
    [SerializeField] private TriggerArea _triggerArea;
    [SerializeField] private RCC_CarControllerV3 _carController;
    [SerializeField] private RCC_AICarController _aiCarController;
    [SerializeField] private RoadCarSpeedSetter _carSpeedSetter;
    [SerializeField] private RoadCarRandomSpeedSetter _carRandomSpeedSetter;

    [Header("Preferences")]
    [SerializeField] private float _speedChangeDuration;
    [SerializeField] private AnimationCurve _speedChangeCurve;
    [SerializeField] private float _speedChangeDelay;

    private PauseEvents _pauseEvents;

    private float _rawSpeed;

    private Tween _speedChangeTween;

    private ConfigurableUpdate _configurableUpdate = new ConfigurableUpdate();

    [Inject]
    private void Construct(PauseEvents _pauseEvents)
    {
        this._pauseEvents = _pauseEvents;
    }

    #region MonoBehaviour

    private void OnValidate()
    {
        _transform ??= GetComponent<Transform>();
        _triggerArea ??= GetComponent<TriggerArea>();
        _aiCarController ??= transform.parent.GetComponent<RCC_AICarController>();
        _carController ??= transform.parent.GetComponent<RCC_CarControllerV3>();
        _carSpeedSetter ??= transform.parent.GetComponent<RoadCarSpeedSetter>();
        _carRandomSpeedSetter ??= transform.parent.GetComponent<RoadCarRandomSpeedSetter>();
    }

    private void Awake()
    {
        _rawSpeed = _aiCarController.maximumSpeed;

        _configurableUpdate.Init(this, _speedChangeDelay, TailgateRoutine);
    }
    private void OnEnable()
    {
        _triggerArea.onFill += StartTailgating;
        _triggerArea.onEmpty += StopTailgating;
        _carRandomSpeedSetter.onSet += UpdateRawSpeed;
    }

    private void OnDisable()
    {
        _triggerArea.onFill -= StartTailgating;
        _triggerArea.onEmpty -= StopTailgating;
        _carRandomSpeedSetter.onSet -= UpdateRawSpeed;

        _speedChangeTween.Kill();
    }

    #endregion

    private void UpdateRawSpeed(float speed)
    {
        _rawSpeed = speed;
    }

    private void StartTailgating()
    {
        _configurableUpdate.StartUpdating();
    }

    private void StopTailgating()
    {
        _configurableUpdate.StopUpdating();

        SetSpeedSmoothly(_rawSpeed);
    }

    private void TailgateRoutine()
    {
        if (_pauseEvents.isPaused) return;

        Transform closestCar = GetClosestCar();
        
        float speed = GetSpeed(closestCar);

        SetSpeedSmoothly(speed);
    }
    private Transform GetClosestCar()
    {
        Transform[] transforms = _triggerArea.affectedObjects.Select(x => x.transform).ToArray();

        Transform car = _transform.FindClosestTransform(transforms);

        return car;
    }

    private float GetSpeed(Transform car)
    {
        return Mathf.Clamp(car.parent.parent.GetComponent<RCC_CarControllerV3>().speed, 0, _rawSpeed);
    }

    private void SetSpeedSmoothly(float speed)
    {
        _speedChangeTween.Kill();

        _speedChangeTween = DOTween
            .To(() => _carSpeedSetter.Speed, x => _carSpeedSetter.SetSpeed(x), speed, _speedChangeDuration)
            .SetEase(_speedChangeCurve);
    }
}
