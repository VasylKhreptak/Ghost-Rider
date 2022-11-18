using UnityEngine;
using Zenject;

public class CameraInstaller : MonoInstaller
{
    [Header("References")]
    [SerializeField] private Camera _camera;

    [Header("Preferences")]
    [SerializeField] private Cameras _cameraType;
    
    public override void InstallBindings()
    {
        Container.BindInstance(_camera).WithId(_cameraType).AsSingle();
    }
}