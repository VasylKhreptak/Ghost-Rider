using System;
using System.Collections;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class CarSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TriggerArea _triggerArea;

    [Header("Preferences")]
    [SerializeField] private Pools[] _cars;
    [SerializeField] private Vector3 _minOffset;
    [SerializeField] private Vector3 _maxOffset;
    [SerializeField] private float _minSpawnDelay;
    [SerializeField] private float _maxSpawnDelay;
    [SerializeField, Range(0f, 1f)] private float _spawnProbability;

    private Vector3 _offset => Extensions.Random.Range(_minOffset, _maxOffset);
    private float _spawnDelay => Random.Range(_minSpawnDelay, _maxSpawnDelay);

    private Coroutine _spawnCoroutine;

    private ObjectPooler _objectPooler;

    private PauseEvents _pauseEvents;

    [Inject]
    private void Construct(ObjectPooler objectPooler, PauseEvents pauseEvents)
    {
        _objectPooler = objectPooler;
        _pauseEvents = pauseEvents;
    }

    #region MonoBehaviour

    private void OnValidate()
    {
        _triggerArea ??= GetComponent<TriggerArea>();
    }

    #endregion

    public void StartSpawning()
    {
        if (_spawnCoroutine == null)
        {
            _spawnCoroutine = StartCoroutine(SpawnRoutine());
        }
    }

    public void StopSpawning()
    {
        if (_spawnCoroutine != null)
        {
            StopCoroutine(_spawnCoroutine);

            _spawnCoroutine = null;
        }
    }

    private IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(_spawnDelay);

        while (true)
        {
            if (CanSpawn())
            {
                Spawn();
            }

            yield return new WaitForSeconds(_spawnDelay);
        }
    }

    private bool CanSpawn()
    {
        return _triggerArea.IsEmpty
            && _pauseEvents.isPaused == false
            && Extensions.Mathf.Probability(_spawnProbability);
    }

    private void Spawn()
    {
        _objectPooler.TrySpawnInactive(out var poolObject, _cars.Random(),
            _triggerArea.gameObject.transform.position + _offset, Quaternion.identity);
    }
}
