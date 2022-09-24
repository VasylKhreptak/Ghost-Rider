using UnityEngine;
using UnityEngine.UI;

public class VSyncToggle : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Toggle _toggle;

    #region MonoBehaciour

    private void OnValidate()
    {
        _toggle ??= GetComponent<Toggle>();
    }
    private void OnEnable()
    {
        _toggle.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveListener(OnValueChanged);
    }

    #endregion

    private void OnValueChanged(bool value)
    {
        QualitySettings.vSyncCount = value ? 1 : 0;
    }
}
