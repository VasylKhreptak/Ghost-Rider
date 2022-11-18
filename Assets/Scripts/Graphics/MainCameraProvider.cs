using UnityEngine;
using Zenject;

public class MainCameraProvider : CameraProvider
{
    [Inject(Id = Cameras.Main)] public override Camera Camera
    {
        get;
        protected set;
    }
}
