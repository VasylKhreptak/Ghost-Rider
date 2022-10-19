using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CarOvertaking : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TriggerArea _tailgatingArea;
    [SerializeField] private TriggerArea _leftSide;
    [SerializeField] private TriggerArea _rightSide;
    [SerializeField] private CarWaypointPositionLinker _positionLinker;

    [Header("Preferences")]
    [SerializeField] private float _distanceBetweenLanes = 5f;
    [SerializeField] private float _minOvertakeDelay;
    [SerializeField] private float _maxOvertakeDelay;
    [SerializeField] private float _minOvertakeStartDelay;
    [SerializeField] private float _maxOvertakeStartDelay;
    [SerializeField] private float _minOvertakeRetryDelay;
    [SerializeField] private float _maxOvertakeRetryDelay;
    [SerializeField] private float _minRoadX = -7f;
    [SerializeField] private float _maxRoadX = 7f;

    private float OvertakeStartDelay => Random.Range(_minOvertakeStartDelay, _maxOvertakeStartDelay);
    private float OvertakeRetryDelay => Random.Range(_minOvertakeRetryDelay, _maxOvertakeRetryDelay);
    private float OvertakeDelay => Random.Range(_minOvertakeDelay, _maxOvertakeDelay);

    private Coroutine _overtakeCoroutine;

    private int _overtakeSide;

    #region MonoBehaviour

    private void OnValidate()
    {
        _positionLinker ??= GetComponentInChildren<CarWaypointPositionLinker>();
    }

    private void OnEnable()
    {
        _tailgatingArea.onFill += StartOvertaking;
        _tailgatingArea.onEmpty += StopOvertaking;
        _leftSide.onFill += UpdateOvertakeSide;
        _rightSide.onFill += UpdateOvertakeSide;
        _leftSide.onEmpty += UpdateOvertakeSide;
        _rightSide.onEmpty += UpdateOvertakeSide;
        _tailgatingArea.onFill += UpdateOvertakeSide;
    }

    private void OnDisable()
    {
        _tailgatingArea.onFill -= StartOvertaking;
        _tailgatingArea.onEmpty -= StopOvertaking;
        _leftSide.onFill -= UpdateOvertakeSide;
        _rightSide.onFill -= UpdateOvertakeSide;
        _leftSide.onEmpty -= UpdateOvertakeSide;
        _rightSide.onEmpty -= UpdateOvertakeSide;
        _tailgatingArea.onFill -= UpdateOvertakeSide;
    }

    #endregion

    private void StartOvertaking()
    {
        if (_overtakeCoroutine == null)
        {
            _overtakeCoroutine = StartCoroutine(OvertakeRoutine());
        }
    }

    private void StopOvertaking()
    {
        if (_overtakeCoroutine != null)
        {
            StopCoroutine(_overtakeCoroutine);

            _overtakeCoroutine = null;

            _overtakeSide = 0;
        }
    }

    private IEnumerator OvertakeRoutine()
    {
        _overtakeSide = Extensions.Random.Sign();

        yield return new WaitForSeconds(OvertakeStartDelay);

        while (true)
        {
            if (TryOvertake())
            {
                yield return new WaitForSeconds(OvertakeDelay);
            }
            else
            {
                yield return new WaitForSeconds(OvertakeRetryDelay);
            }
        }
    }

    private bool TryOvertake()
    {
        bool canOvertake = CanOvertake();

        if (CanOvertake())
        {
            Overtake();
        }

        return canOvertake;
    }

    private bool CanOvertake()
    {
        return _leftSide.IsEmpty || _rightSide.IsEmpty;
    }

    private void Overtake()
    {
        if (_overtakeSide == -1)
        {
            _positionLinker.offset.x -= _distanceBetweenLanes;
        }
        else if (_overtakeSide == 1)
        {
            _positionLinker.offset.x += _distanceBetweenLanes;
        }

        _positionLinker.offset.x = Mathf.Clamp(_positionLinker.offset.x, _minRoadX, _maxRoadX);
    }

    private void UpdateOvertakeSide()
    {
        if (CanOvertake() == false)
        {
            _overtakeSide = 0;
        }
        else if (_leftSide.IsEmpty && _rightSide.IsEmpty)
        {
            _overtakeSide = Extensions.Random.Sign();
        }
        else
        {
            if (_leftSide.IsEmpty)
            {
                _overtakeSide = -1;
            }
            else
            {
                _overtakeSide = 1;
            }
        }
    }
}
