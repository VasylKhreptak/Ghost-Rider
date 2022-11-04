using UnityEngine;
using Zenject;

public class PlayEventInstaller : MonoInstaller
{
    [Header("References")]
    [SerializeField] private OnPlayEvent _onPlayEvent;

    public override void InstallBindings()
    {
        Container.BindInstance(_onPlayEvent).AsSingle();
    }
}