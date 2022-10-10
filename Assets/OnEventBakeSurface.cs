using UnityEngine;
using Zenject;

public class OnEventBakeSurface : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MonoEvent _event;

    private SurfaceBaker _surfaceBaker;

    [Inject]
    private void Construct(SurfaceBaker surfaceBaker)
    {
        _surfaceBaker = surfaceBaker;
    }

    #region MonoBehaviour

    private void OnValidate()
    {
        _event ??= GetComponent<MonoEvent>();
    }

    private void OnEnable()
    {
        _event.onMonoCall += _surfaceBaker.Bake;
    }

    private void OnDisable()
    {
        _event.onMonoCall -= _surfaceBaker.Bake;
    }

    #endregion
}
