using UnityEngine;
public class GetKeyDownMonoEvent : MonoEvent
{
    [Header("Preferences")]
    [SerializeField] private KeyCode _keyCode;
    
    public bool Enabled = true;
    
    #region MonoBehaviour

    private void Update()
    {
        if (Enabled == false) return;

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
