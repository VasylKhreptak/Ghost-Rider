using UnityEngine;
using Zenject;

public class OnEventAddScore : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private OnTriggerEnterEvent _triggerEnterEvent;
    [SerializeField] private RCC_CarControllerV3 _carController;

    [Header("Preferences")]
    [SerializeField] private float _minSpeedDifference = 20f;
    [SerializeField] private int _minScore = 30;
    [SerializeField] private int _maxScore = 100;
    [SerializeField] private AnimationCurve _scoreCurve;

    private ScoreCounter _scoreCounter;

    [Inject]
    private void Construct(ScoreCounter scoreCounter)
    {
        _scoreCounter = scoreCounter;
    }
    
    #region MonoBehaviour

    private void OnValidate()
    {
        _triggerEnterEvent ??= GetComponent<OnTriggerEnterEvent>();
    }

    private void OnEnable()
    {
        _triggerEnterEvent.onEnter += TryAddScore;
    }

    private void OnDisable()
    {
        _triggerEnterEvent.onEnter -= TryAddScore;
    }

    #endregion

    private void TryAddScore(Collider collider)
    {
        if (collider.transform.parent.parent.TryGetComponent(out RCC_CarControllerV3 carController) &&
            (carController.speed - _carController.speed) > _minSpeedDifference)
        {
            int score = GetScore(_carController.speed, carController.maxspeed, carController.speed);
            
            _scoreCounter.AddScoreWithBonuses(score);
        }
    }

    private int GetScore(float minSpeed, float maxSpeed, float currentSpeed)
    {
        return (int)_scoreCurve.Evaluate(minSpeed, maxSpeed, currentSpeed, _minScore, _maxScore);
    }
}
