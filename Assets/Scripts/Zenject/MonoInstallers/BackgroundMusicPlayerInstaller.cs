using UnityEngine;
using Zenject;

public class BackgroundMusicPlayerInstaller : MonoInstaller
{
    [Header("References")]
    [SerializeField] private GameObject _prefab;

    public override void InstallBindings()
    {
        GameObject instance = Container.InstantiatePrefab(_prefab);
        instance.transform.SetParent(null);
        DontDestroyOnLoad(instance);
        Container.Bind<BackgroundMusicPlayer>().FromComponentOn(instance).AsSingle();
    }
}
