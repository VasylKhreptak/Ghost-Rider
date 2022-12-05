using UnityEngine;
using Zenject;

public class BackgroundTrackProviderInstaller : MonoInstaller
{
    [Header("References")]
    [SerializeField] private GameObject _prefab;

    public override void InstallBindings()
    {
        GameObject instance = Container.InstantiatePrefab(_prefab);
        instance.transform.SetParent(null);
        DontDestroyOnLoad(instance);
        Container.Bind<BackgroundTrackProvider>().FromComponentOn(instance).AsSingle();
    }
}