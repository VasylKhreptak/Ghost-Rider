using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class PauseEventsHolderInstaller : MonoInstaller
{
    [FormerlySerializedAs("_pauseEvents")]
    [FormerlySerializedAs("_pauseEventsHolder")]
    [Header("References")]
    [SerializeField] private PauseManager _pauseManager;
    
    public override void InstallBindings()
    {
        Container.Bind<PauseManager>().FromInstance(_pauseManager).AsSingle();
    }
}