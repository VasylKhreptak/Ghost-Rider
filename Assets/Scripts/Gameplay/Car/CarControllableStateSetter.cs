using UnityEngine;

public class CarControllableStateSetter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RCC_CarControllerV3 _car;

    public void SetControllableState(bool canControl)
    {
        _car.canControl = canControl;
    }
}
