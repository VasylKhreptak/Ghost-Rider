using System;
using UnityEngine;

public class GameFramerate : MonoBehaviour
{
    [Header("Preferences")]
    [SerializeField] private int _defaultFramerate = 60;
    [SerializeField] private bool _editMode;

    [Header("PlayerPrefs Preferences")]
    [SerializeField] private string _key = "GameFramerate";

    #region MonoBehaviour

    private void OnValidate()
    {
        if (_editMode)
        {
            Set(_defaultFramerate);
            
            return;
        }
        
        Set(Application.targetFrameRate = Int32.MaxValue);
    }

    private void Awake()
    {
        Set(PlayerPrefsSafe.GetInt(_key, _defaultFramerate));

        QualitySettings.vSyncCount = 0;
    }

    #endregion

    public void Set(int framerate)
    {
        Application.targetFrameRate = framerate;
    }
}
