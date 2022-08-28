using UnityEngine;

public class ScreenSleep : MonoBehaviour
{
    [Header("Preferences")]
    [SerializeField] private bool _enableScreenSleep;

    #region MonoBehaviour

    private void Start()
    {
        Screen.sleepTimeout = _enableScreenSleep ? SleepTimeout.SystemSetting : SleepTimeout.NeverSleep;
    }

    #endregion
}