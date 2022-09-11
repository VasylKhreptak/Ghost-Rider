using UnityEngine;
public class GetKeyDownMonoEvent : MonoEvent
{
    [Header("Preferences")]
    [SerializeField] private KeyCode _keyCode;
    
    public bool State = true;
    
    #region MonoBehaviour

    private void Update()
    {
        if (State == false) return;

        CheckPressedState();
    }

    #endregion

    private void CheckPressedState()
    {
        if (Input.GetKeyDown(_keyCode))
        {
            onMonoCall?.Invoke();
        }
    }
}
