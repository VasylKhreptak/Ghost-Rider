using UnityEngine;

public class OnMouseButtonDownMonoEvent : MonoEvent
{
    [Header("Preferences")]
    [SerializeField] private int _buttonId;
    
    #region MonoBehaviour

    private void Update()
    {
        if (Input.GetMouseButtonDown(_buttonId))
        {
            onMonoCall?.Invoke();
        }
    }

    #endregion
}
