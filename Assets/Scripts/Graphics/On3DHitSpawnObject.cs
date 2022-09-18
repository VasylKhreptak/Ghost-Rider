using UnityEngine;
using Zenject;

public class On3DHitSpawnObject : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private OnCollisionEnterEvent _collisionEnterEvent;

    [Header("Preferences")]
    [SerializeField] private Pools[] _pools;

    private ObjectPooler _objectPooler;

    [Inject]
    private void Construct(ObjectPooler objectPooler)
    {
        _objectPooler = objectPooler;
    }
    
    #region MonoBehaviour

    private void OnValidate()
    {
        _collisionEnterEvent ??= GetComponent<OnCollisionEnterEvent>();
    }

    private void OnEnable()
    {
        _collisionEnterEvent.onEnter += SpawnObject;
    }

    private void OnDisable()
    {
        _collisionEnterEvent.onEnter -= SpawnObject;
    }

    #endregion

    private void SpawnObject(Collision collision)
    {
        Vector3 position = collision.GetContact(0).point;

        _objectPooler.Spawn(_pools.Random(), position, Quaternion.identity);
    }
}
