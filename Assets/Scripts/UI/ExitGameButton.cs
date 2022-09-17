using UnityEngine;
using UnityEngine.UI;

public class ExitGameButton : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Button _button;

    #region MonoBehaviour

    private void OnValidate()
    {
        _button ??= GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(Exit);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Exit);
    }

    #endregion

    private void Exit()
    {
        Application.Quit();
    }
}
