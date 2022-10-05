using UnityEngine;

public class OnWaypointEnterMonoEvent : MonoEvent
{
    [Header("References")]
    [SerializeField] private RCC_AICarController _aiCarController;

    [Header("Preferences")]
    [SerializeField] private int _targetWaypointIndex;

    #region MonoBehaviour

    private void OnValidate()
    {
        _aiCarController ??= GetComponent<RCC_AICarController>();
    }

    private void Awake()
    {
        _aiCarController.onWaypointEnter += TryInvoke;
    }

    private void OnDestroy()
    {
        _aiCarController.onWaypointEnter -= TryInvoke;
    }

    #endregion

    private void TryInvoke(int waypointIndex)
    {
        if (CanInvoke(waypointIndex))
        {
            Invoke();
        }
    }

    private bool CanInvoke(int waypointIndex)
    {
        return waypointIndex == _targetWaypointIndex;
    }

    public void SetAIController(RCC_AICarController controller)
    {
        _aiCarController = controller;
        
        _aiCarController.onWaypointEnter += TryInvoke;
    }
}
