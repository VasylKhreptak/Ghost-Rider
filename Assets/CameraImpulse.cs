using System;
using UnityEngine;

public class CameraImpulse : MonoBehaviour
{
    private void Start()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(Vector3.forward * 100f, ForceMode.Impulse);
    }
}
