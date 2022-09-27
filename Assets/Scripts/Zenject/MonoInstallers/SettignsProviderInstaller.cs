using UnityEngine;
using Zenject;

public class SettignsProviderInstaller : MonoInstaller
{
    [Header("References")]
    [SerializeField] private SettingsProvider _settingsProvider;

    public override void InstallBindings()
    {
        Container.Bind<SettingsProvider>().FromInstance(_settingsProvider).AsSingle();
    }
}
