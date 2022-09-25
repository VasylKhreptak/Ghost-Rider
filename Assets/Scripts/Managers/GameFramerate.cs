using System;
using UnityEngine;

public class GameFramerate : MonoBehaviour
{
    [Header("Preferences")]
    [SerializeField] private int _defaultFramerate = 60;

    #region Monobehaviour

    private void Awake()
    {
        Set(_defaultFramerate);

        QualitySettings.vSyncCount = 0;
    }

    #endregion

    public void Set(int framerate)
    {
        Application.targetFrameRate = framerate;
    }
}
