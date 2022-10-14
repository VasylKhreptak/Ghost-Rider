using UnityEngine;

public class RoadCarSpeedSetter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RCC_AICarController _aiCarController;
    [SerializeField] private RCC_Waypoint _waypoint;

    #region MonoBehaviour

    protected virtual void OnValidate()
    {
        _aiCarController ??= transform.GetComponentInChildren<RCC_AICarController>();
        _waypoint ??= transform.GetComponentInChildren<RCC_Waypoint>();
    }

    #endregion

    public void SetSpeed(float speed)
    {
        _waypoint.targetSpeed = speed;
        _aiCarController.maximumSpeed = speed;
    }
}
