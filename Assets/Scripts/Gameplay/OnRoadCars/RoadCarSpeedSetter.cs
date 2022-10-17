using System;
using UnityEngine;

public class RoadCarSpeedSetter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RCC_AICarController _aiCarController;
    [SerializeField] private RCC_CarControllerV3 _carController;
    [SerializeField] private RCC_Waypoint _waypoint;

    public float Speed => _carController.speed;
    
    public Action<float> onSet;

    #region MonoBehaviour

    protected virtual void OnValidate()
    {
        _aiCarController ??= transform.GetComponentInChildren<RCC_AICarController>();
        _waypoint ??= transform.GetComponentInChildren<RCC_Waypoint>();
        _carController ??= transform.GetComponent<RCC_CarControllerV3>();
    }

    #endregion

    public void SetSpeed(float speed)
    {
        _waypoint.targetSpeed = speed;
        _aiCarController.maximumSpeed = speed;
        _carController.maxspeed = speed;
        
        onSet?.Invoke(speed);
    }
}
