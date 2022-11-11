using UnityEngine;

public class CameraCarSelector : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RCC_Camera _rccCamera;
    [SerializeField] private MainCarSpawner _mainCarSpawner;
    
    public void AimCameraToMainCar()
    {
        _rccCamera.playerCar = _mainCarSpawner.CurrentCar.CarController;
    }

    public void ResetCarCamera()
    {
        _rccCamera.playerCar = null;
    }
}
