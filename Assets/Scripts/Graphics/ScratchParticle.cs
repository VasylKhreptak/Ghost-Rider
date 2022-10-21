using UnityEngine;
using Zenject;

public class ScratchParticle : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ScratchEvents _scratchEvents;

    [Header("Preferences")]
    [SerializeField] private Pools _particlePool;

    private ObjectPooler _objectPooler;

    private ParticleSystem _particle;

    [Inject]
    private void Construct(ObjectPooler objectPooler)
    {
        _objectPooler = objectPooler;
    }

    #region MonoBehaviour

    private void OnValidate()
    {
        _scratchEvents ??= GetComponent<ScratchEvents>();
    }

    private void OnEnable()
    {
        _scratchEvents.onStartScratching += OnStartScratching;
        _scratchEvents.onScratchUpdate += OnScratchUpdate;
        _scratchEvents.onStopScratching += OnStopScratching;
    }

    private void OnDisable()
    {
        _scratchEvents.onStartScratching -= OnStartScratching;
        _scratchEvents.onScratchUpdate -= OnScratchUpdate;
        _scratchEvents.onStopScratching -= OnStopScratching;
    }

    #endregion

    private void OnStartScratching(Vector3 point)
    {
        SpawnParticle(ref point);
    }

    private void OnScratchUpdate(Vector3 point)
    {
        UpdateParticlePosition(ref point);
    }

    private void OnStopScratching()
    {
        DisableParticle();
    }

    private void SpawnParticle(ref Vector3 point)
    {
        GameObject particleObject = _objectPooler.Spawn(_particlePool, point, Quaternion.identity);
        _particle = particleObject.GetComponent<ParticleSystem>();

        ParticleSystem.MainModule module = _particle.main;
        module.loop = true;
    }

    private void DisableParticle()
    {
        ParticleSystem.MainModule module = _particle.main;
        module.loop = false;
        _particle = null;
    }

    private void UpdateParticlePosition(ref Vector3 position)
    {
        _particle.transform.position = position;
    }
}
