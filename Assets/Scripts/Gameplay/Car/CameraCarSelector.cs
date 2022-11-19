using UnityEngine;

public class CameraCarSelector : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RCC_Camera _rccCamera;
    [SerializeField] private MainCarSpawner _mainCarSpawner;
    
    public void AimCameraToMainCar()
    {
        _rccCamera.SetTarget(_mainCarSpawner.CurrentCar.GameObject);
    }

    public void ResetCarCamera()
    {
        _rccCamera.RemoveTarget();
    }
}
