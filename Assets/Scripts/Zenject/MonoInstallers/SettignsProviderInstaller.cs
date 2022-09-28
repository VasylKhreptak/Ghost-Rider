using UnityEngine;
using Zenject;

public class SettignsProviderInstaller : MonoInstaller
{
    [Header("References")]
    [SerializeField] private GameObject _settingsProviderPrefab;

    public override void InstallBindings()
    {
        GameObject settingsProvider = GameObjectExtensions.InstantiateDontDestroyOnLoad(_settingsProviderPrefab);

        Container.Bind<SettingsProvider>().FromComponentOn(settingsProvider).AsSingle();
    }
}
