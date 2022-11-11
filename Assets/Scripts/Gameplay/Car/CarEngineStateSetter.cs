using UnityEngine;

public class CarEngineStateSetter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RCC_CarControllerV3 _carController;

    public void SetState(bool enabled)
    {
        _carController.SetEngine(enabled);
    }
}
