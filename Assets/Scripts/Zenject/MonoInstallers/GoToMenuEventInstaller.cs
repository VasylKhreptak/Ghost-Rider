using UnityEngine;
using Zenject;

public class GoToMenuEventInstaller : MonoInstaller
{
    [Header("References")]
    [SerializeField] private GoToMenuEvent _goToMenuEvent;
    
    public override void InstallBindings()
    {
        Container.BindInstance(_goToMenuEvent).AsSingle();
    }
}