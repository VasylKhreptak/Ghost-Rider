using UnityEngine;
using Zenject;

public class PauseEventsHolderInstaller : MonoInstaller
{
    [Header("References")]
    [SerializeField] private PauseEventsHolder _pauseEventsHolder;
    
    public override void InstallBindings()
    {
        Container.Bind<PauseEventsHolder>().FromInstance(_pauseEventsHolder).AsSingle();
    }
}