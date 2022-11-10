using UnityEngine;
using Zenject;

public class MainWaypointsContainerInstaller : MonoInstaller
{
    [Header("References")]
    [SerializeField] private RCC_AIWaypointsContainer _waypointsContainer;

    public override void InstallBindings()
    {
        Container.BindInstance(_waypointsContainer).AsSingle();
    }
}