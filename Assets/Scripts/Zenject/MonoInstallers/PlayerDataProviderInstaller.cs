using UnityEngine;
using Zenject;

public class PlayerDataProviderInstaller : MonoInstaller
{
    [Header("References")]
    [SerializeField] private PlayerDataProvider _playerDataProvider;

    public override void InstallBindings()
    {
        Container.BindInstance(_playerDataProvider).AsSingle();
    }
}