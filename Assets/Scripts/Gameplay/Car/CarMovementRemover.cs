using UnityEngine;

public class CarMovementRemover : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RCC_CarControllerV3 _car;

    public void CanMove(bool canMove)
    {
        Rigidbody rigidbody = _car.gameObject.GetComponent<Rigidbody>();

        rigidbody.constraints = canMove ? RigidbodyConstraints.None : RigidbodyConstraints.FreezeAll;
    }
}
