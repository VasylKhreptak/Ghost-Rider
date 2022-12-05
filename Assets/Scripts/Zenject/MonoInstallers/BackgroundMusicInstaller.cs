using UnityEngine;
using Zenject;

public class BackgroundMusicInstaller : MonoInstaller
{
    [Header("References")]
    [SerializeField] private GameObject _prefab;

    public override void InstallBindings()
    {
        GameObject instance = GameObjectExtensions.InstantiateDontDestroyOnLoad(_prefab);
        Container.Bind<BackgroundMusicPlayer>().FromComponentOn(instance).AsSingle();
    }
}
