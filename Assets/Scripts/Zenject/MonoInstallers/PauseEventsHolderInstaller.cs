using UnityEngine;
using Zenject;

public class PauseEventsHolderInstaller : MonoInstaller
{
    [Header("References")]
    [SerializeField] private PauseEventsHolder _pauseEventsHolder;
    
    public override void InstallBindings()
    {
        Container.BindInstance(_pauseEventsHolder).AsSingle();
    }
}