using UnityEngine;
using Zenject;

public class SurfaceBakerInstaller : MonoInstaller
{
    [Header("References")]
    [SerializeField] private SurfaceBaker _surfaceBaker;
    
    public override void InstallBindings()
    {
        Container.BindInstance(_surfaceBaker).AsSingle();
    }
}