using ModestTree;
using TMPro;
using UnityEngine;

public class FullScreenModeDropdown : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Dropdown _dropdown;

    private readonly FullScreenMode[] _screenModes =
    {
        FullScreenMode.Windowed,
        FullScreenMode.ExclusiveFullScreen,
        FullScreenMode.FullScreenWindow
    };

    #region MonoBehaviour

    private void Awake()
    {
        _dropdown.SetValueWithoutNotify(_screenModes.IndexOf(Screen.fullScreenMode));
    }

    private void OnValidate()
    {
        _dropdown ??= GetComponent<TMP_Dropdown>();
    }

    private void OnEnable()
    {
        _dropdown.onValueChanged.AddListener(SetFullScreenMode);
    }

    private void OnDisable()
    {
        _dropdown.onValueChanged.RemoveListener(SetFullScreenMode);
    }

    #endregion

    private void SetFullScreenMode(int index)
    {
        Screen.fullScreenMode = _screenModes[index];
    }
}
