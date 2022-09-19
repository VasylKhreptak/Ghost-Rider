using System.Collections;
using DG.Tweening;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class FlyingVehicleSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private OccludeeEvents _occludeeEvents;

    [Header("Preferences")]
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Pools[] _vehicles;

    [Header("Fly Preferences")]
    [SerializeField] private float _minDuration;
    [SerializeField] private float _maxDuration;
    [SerializeField] private Vector3 _minOffset;
    [SerializeField] private Vector3 _maxOffset;
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private float _minSpawnDelay;
    [SerializeField] private float _maxSpawnDelay;
    [SerializeField] private float _minStartDelay;
    [SerializeField] private float _maxStartDelay;

    private float _duration => Random.Range(_minDuration, _maxDuration);
    private float _spawnDelay => Random.Range(_minSpawnDelay, _maxSpawnDelay);
    private float _startDelay => Random.Range(_minStartDelay, _maxStartDelay);

    private ObjectPooler _objectPooler;

    private Tween _moveTween;

    private Coroutine _spawnCoroutine;

    private GameObject _currentVehicle;

    private PauseEventsHolder _pauseEventsHolder;

    [Inject]
    private void Construct(ObjectPooler objectPooler, PauseEventsHolder pauseEventsHolder)
    {
        _objectPooler = objectPooler;
        _pauseEventsHolder = pauseEventsHolder;
    }

    #region MonoBehaviour

    private void OnValidate()
    {
        _occludeeEvents ??= GetComponent<OccludeeEvents>();
    }

    private void Awake()
    {
        _occludeeEvents.onBecameInvisible += OnBecameInvisible;
        _occludeeEvents.onBecameVisible += OnBecameVisible;
    }

    private void OnEnable()
    {
        StartSpawning();

        _pauseEventsHolder.onPause += OnPause;
        _pauseEventsHolder.onResume += OnResume;
    }

    private void OnDisable()
    {
        StopSpawning();

        Kill();

        _pauseEventsHolder.onPause -= OnPause;
        _pauseEventsHolder.onResume -= OnResume;
    }

    private void OnDestroy()
    {
        _occludeeEvents.onBecameInvisible -= OnBecameInvisible;
        _occludeeEvents.onBecameVisible -= OnBecameVisible;
    }

    #endregion

    private void StartSpawning()
    {
        if (_spawnCoroutine == null)
        {
            _spawnCoroutine = StartCoroutine(SpawnRoutine());
        }
    }

    private void StopSpawning()
    {
        if (_spawnCoroutine != null)
        {
            StopCoroutine(_spawnCoroutine);

            _spawnCoroutine = null;
        }
    }

    private IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(_startDelay);

        while (true)
        {
            if (_pauseEventsHolder.isPaused == false)
            {
                float duration = _duration;
                Spawn(duration);

                yield return new WaitForSeconds(duration);
            }


            yield return new WaitForSeconds(_spawnDelay);
        }
    }

    private void Spawn(float flyDuration)
    {
        int index = Random.Range(0, 2);

        Vector3 from = GetSpawnPlace(_spawnPoints[index].position);
        Vector3 to = GetSpawnPlace(_spawnPoints[index == 0 ? 1 : 0].position);

        Quaternion rotation = Quaternion.LookRotation(to - from);

        GameObject vehicle = _objectPooler.Spawn(_vehicles.Random(), _spawnPoints[0].position, rotation);
        _currentVehicle = vehicle;

        vehicle.transform.DOKill();

        vehicle.transform.position = from;
        _moveTween = vehicle.transform.DOMove(to, flyDuration).SetEase(_animationCurve).OnComplete(() =>
        {
            vehicle.SetActive(false);
        });
    }

    private Vector3 GetSpawnPlace(Vector3 center)
    {
        Vector3 spawnPlace = center;

        spawnPlace.x += Random.Range(_minOffset.x, _maxOffset.x);
        spawnPlace.y += Random.Range(_minOffset.y, _maxOffset.y);
        spawnPlace.z += Random.Range(_minOffset.z, _maxOffset.z);

        return spawnPlace;
    }

    private void Kill()
    {
        _moveTween.Kill();

        if (_currentVehicle != null)
        {
            _currentVehicle.SetActive(false);
        }
    }

    private void OnBecameInvisible()
    {
        this.enabled = false;
    }

    private void OnBecameVisible()
    {
        this.enabled = true;
    }

    private void OnPause()
    {
        _moveTween.Pause();
    }

    private void OnResume()
    {
        _moveTween.Play();
    }

    private void OnDrawGizmosSelected()
    {
        if (_spawnPoints[0] == null || _spawnPoints[1] == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_spawnPoints[0].position, 2f);
        Gizmos.DrawWireSphere(_spawnPoints[1].position, 2f);

        void DrawOffset(Transform center)
        {
            Vector3 position = center.position;

            Gizmos.DrawLine(position + new Vector3(_minOffset.x, 0, 0), position + new Vector3(_maxOffset.x, 0, 0));
            Gizmos.DrawLine(position + new Vector3(0, _minOffset.y, 0), position + new Vector3(0, _maxOffset.y, 0));
            Gizmos.DrawLine(position + new Vector3(0, 0, _minOffset.z), position + new Vector3(0, 0, _maxOffset.z));
        }

        DrawOffset(_spawnPoints[0]);
        DrawOffset(_spawnPoints[1]);
    }
}
