using System;
using UnityEngine;
using UnityEngine.UI;

public class CarSelectButton : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Button _button;
    [SerializeField] protected MainCarSpawner _mainCarSpawner;

    [Header("Preferences")]
    [SerializeField] protected Pools[] _cars;

    #region MonoBehaviour

    private void OnValidate()
    {
        _button ??= GetComponent<Button>();
        _mainCarSpawner ??= FindObjectOfType<MainCarSpawner>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(SelectCar);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(SelectCar);
    }

    #endregion

    protected virtual void SelectCar()
    {
        throw new NotImplementedException();
    }
}
