using UnityEngine;
using Zenject;

public class NetworkConnectionEventsInstaller : MonoInstaller
{
    [Header("References")]
    [SerializeField] private GameObject _prefab;
    
    public override void InstallBindings()
    {
        GameObject instantiatedObject = GameObjectExtensions.InstantiateDontDestroyOnLoad(_prefab);
        Container.Bind<NetworkConnectionEvents>().FromComponentOn(instantiatedObject).AsSingle();
    }
}