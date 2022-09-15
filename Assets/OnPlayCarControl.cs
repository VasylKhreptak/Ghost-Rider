using UnityEngine;

public class OnPlayCarControl : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MonoEvent _event;
    [SerializeField] private RCC_CarControllerV3 _carController;
    
    #region MonoBehaviour

    private void Awake()
    {
        _event.onMonoCall += StartMovement;
    }

    private void OnDestroy()
    {
        _event.onMonoCall -= StartMovement;
    }

    #endregion
    
    private void StartMovement()
    {
        _carController.StartEngine();
    }
}
