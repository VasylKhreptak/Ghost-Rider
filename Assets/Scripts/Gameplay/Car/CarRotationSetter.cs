using UnityEngine;

public class CarRotationSetter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RCC_CarControllerV3 _car;

    [Header("Preferences")]
    [SerializeField] private Vector3 _rotation;
    
    public void SetRotation()
    {
        _car.gameObject.transform.rotation = Quaternion.Euler(_rotation);
    }
}
