using System;
using UnityEngine;

public class CarPositionSetter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RCC_CarControllerV3 _car;

    [Header("Preferences")]
    [SerializeField] private Vector3 _position;

    public void SetPosition()
    {
        _car.gameObject.transform.position = _position;
        _car.gameObject.SetActive(false);
        _car.gameObject.SetActive(true);
    }
}
