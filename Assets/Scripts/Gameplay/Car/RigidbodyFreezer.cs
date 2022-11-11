using UnityEngine;

public class RigidbodyFreezer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody _rigidbody;

    public void Freeze(bool freeze)
    {
        _rigidbody.constraints = freeze ? RigidbodyConstraints.FreezeAll : RigidbodyConstraints.None;
    }
}
