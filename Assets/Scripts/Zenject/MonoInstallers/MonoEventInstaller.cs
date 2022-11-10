using UnityEngine;
using Zenject;

public class MonoEventInstaller : MonoInstaller
{
    [Header("References")]
    [SerializeField] private MonoEvent _event;

    [Header("Preferences")]
    [SerializeField] private MonoEvents _eventType;
    
    public override void InstallBindings()
    {
        Container.BindInstance(_event).WithId(_eventType);
    }
}