using UnityEngine;
using UnityEngine.UIElements;

public class GetKeyDownMonoEvent : MonoEvent
{
    [Header("Preferences")]
    [SerializeField] private KeyCode _keyCode;

    #region MonoBehaviour

    private void Update()
    {
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
