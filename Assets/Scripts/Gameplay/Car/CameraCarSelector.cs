using UnityEngine;

public class CameraCarSelector : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RCC_Camera _rccCamera;
    [SerializeField] private RCC_CarControllerV3 _carController;

    public void AimCameraToMainCar()
    {
        _rccCamera.playerCar = _carController;
    }

    public void ResetCarCamera()
    {
        _rccCamera.playerCar = null;
    }
}
