using UnityEngine;

public class CarEngineStateController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RCC_CarControllerV3 _carController;

    public void SetState(bool enabled)
    {
        if (enabled)
        {
            _carController.StartEngine();
        }
        else
        {
            _carController.KillEngine();
        }
    }
}
