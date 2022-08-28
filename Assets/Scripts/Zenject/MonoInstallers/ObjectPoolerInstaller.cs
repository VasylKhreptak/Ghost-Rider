using UnityEngine;
using Zenject;

public class ObjectPoolerInstaller : MonoInstaller
{
    [Header("References")]
    [SerializeField] private ObjectPooler _objectPooler;
    
    public override void InstallBindings()
    {
        Container.Bind<ObjectPooler>().FromInstance(_objectPooler).AsSingle();
    }
}
 