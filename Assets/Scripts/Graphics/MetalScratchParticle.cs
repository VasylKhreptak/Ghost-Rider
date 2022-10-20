using UnityEngine;
using Zenject;

public class MetalScratchParticle : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private OnCollisionStayEvent _collisionStayEvent;
    [SerializeField] private OnCollisionExitEvent _collisionExitEvent;

    [Header("Preferences")]
    [SerializeField] private Pools _particlePool;
    [SerializeField] private float _minVelocity = 20f;

    private ObjectPooler _objectPooler;

    private ParticleSystem _particle;

    private bool _hasAppropriateVelocity;

    [Inject]
    private void Construct(ObjectPooler objectPooler)
    {
        _objectPooler = objectPooler;
    }

    #region MonoBehaviour

    private void OnValidate()
    {
        _rigidbody ??= GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _collisionStayEvent.onStay += OnStay;
        _collisionExitEvent.onExit += OnExit;
    }

    private void OnDisable()
    {
        _collisionStayEvent.onStay -= OnStay;
        _collisionExitEvent.onExit -= OnExit;
    }

    #endregion

    private void OnStay(Collision collision)
    {
        CheckVelocity(collision);
        
        TrySpawnParticle(collision);

        TryUpdateParticlePosition(collision);

        TryDisableParticleOnStay();
    }

    private void OnExit(Collision collision)
    {
        DisableParticleOnExit();
    }
    
    private void CheckVelocity(Collision collision)
    {
        Rigidbody attachedRigidbody = collision.rigidbody;

        if (attachedRigidbody == null)
        {
            _hasAppropriateVelocity = _rigidbody.velocity.magnitude > _minVelocity;
            
            return;
        }

        float relativeVelocity = Mathf.Abs(_rigidbody.velocity.magnitude - attachedRigidbody.velocity.magnitude);
        
        _hasAppropriateVelocity = relativeVelocity > _minVelocity;
    }

    private void TrySpawnParticle(Collision collision)
    {
        if (CanSpawnParticle())
        {
            SpawnParticle(collision);
        }
    }

    private bool CanSpawnParticle()
    {
        return _particle == null && _hasAppropriateVelocity;
    }

    private void SpawnParticle(Collision collision)
    {
        GameObject particleObject = _objectPooler.Spawn(_particlePool, collision.GetContact(0).point, Quaternion.identity);
        _particle = particleObject.GetComponent<ParticleSystem>();

        ParticleSystem.MainModule module = _particle.main;
        module.loop = true;
    }

    private void TryDisableParticleOnStay()
    {
        if (_particle != null && _hasAppropriateVelocity == false)
        {
           DisableParticle();
        }
    }

    private void DisableParticleOnExit()
    {
        if (_particle == null) return;
        
        DisableParticle();
    }

    private void DisableParticle()
    {
        ParticleSystem.MainModule module = _particle.main;
        module.loop = false;
        _particle = null;
    }
    
    private void TryUpdateParticlePosition(Collision collision)
    {
        if (_particle == null || _hasAppropriateVelocity == false) return;

        _particle.transform.position = collision.GetContact(0).point;
    }
}
