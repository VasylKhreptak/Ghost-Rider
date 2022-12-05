using UnityEngine;
using Zenject;

public class FirebaseConnectionInstaller : MonoInstaller
{
    [Header("References")]
    [SerializeField] private GameObject _prefab;
    
    public override void InstallBindings()
    {
        GameObject instantiatedObject = GameObjectExtensions.InstantiateDontDestroyOnLoad(_prefab);
        Container.Bind<FirebaseConnection>().FromComponentOn(instantiatedObject).AsSingle();
    }
}