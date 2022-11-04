using UnityEngine;
using Zenject;

public class OnEventSpawnObject : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Pools[] _allObjects;
    [SerializeField] private Transform _spawnPlace;
    [SerializeField] private MonoEvent _monoEvent;

    [Header("Preferences")]
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Vector3 _rotation;

    private ObjectPooler _objectPooler;

    [Inject]
    private void Construct(ObjectPooler objectPooler)
    {
        _objectPooler = objectPooler;
    }
    
    #region MonoBehaviour

    private void OnValidate()
    {
        _spawnPlace ??= GetComponent<Transform>();
        _monoEvent ??= GetComponent<MonoEvent>();
    }

    private void OnEnable()
    {
        _monoEvent.onMonoCall += Spawn;
    }

    private void OnDisable()
    {
        _monoEvent.onMonoCall -= Spawn;
    }

    #endregion

    private void Spawn()
    {
        Vector3 position = _spawnPlace.position + _offset;
        Quaternion rotation = Quaternion.Euler(_rotation);

        _objectPooler.Spawn(_allObjects.Random(), position, rotation);
    }
}
