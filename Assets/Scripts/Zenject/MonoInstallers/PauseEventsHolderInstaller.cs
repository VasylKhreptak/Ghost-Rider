using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class PauseEventsHolderInstaller : MonoInstaller
{
    [FormerlySerializedAs("_pauseEventsHolder")]
    [Header("References")]
    [SerializeField] private PauseEvents _pauseEvents;
    
    public override void InstallBindings()
    {
        Container.Bind<PauseEvents>().FromInstance(_pauseEvents).AsSingle();
    }
}