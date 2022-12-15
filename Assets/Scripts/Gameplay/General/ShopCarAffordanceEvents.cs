using System;
using UnityEngine;

public class ShopCarAffordanceEvents : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MainCarSpawner _mainCarSpawner;
    [SerializeField] private PlayerDataProvider _playerDataProvider;
    [SerializeField] private Bank _bank;

    public Action onSpawnedAffordable;
    public Action onSpawnedNotAffordable;
    
    #region MonoBehaviour

    private void OnValidate()
    {
        _mainCarSpawner ??= FindObjectOfType<MainCarSpawner>();
        _playerDataProvider ??= FindObjectOfType<PlayerDataProvider>();
        _bank ??= FindObjectOfType<Bank>();
    }

    private void OnEnable()
    {
        TryInvokeAtStart();
        
        _mainCarSpawner.onSpawn += TryInvoke;
    }

    private void OnDisable()
    {
        _mainCarSpawner.onSpawn -= TryInvoke;
    }

    #endregion

    private void TryInvoke(MainCar mainCar)
    {
        bool isAffordable = CanAfford(mainCar);
        
        (isAffordable ? onSpawnedAffordable : onSpawnedNotAffordable)?.Invoke();
    }

    private bool CanAfford(MainCar car)
    {
        return _bank.CanSpendMoney(car.CarSpecification.Price) && IsCarBought(car) == false;
    }

    private bool IsCarBought(MainCar car)
    {
        foreach (var boughtCar in _playerDataProvider.playerData.boughtCars)
        {
            if (boughtCar == car.Pool)
            {
                return true;
            }
        }

        return false;
    }

    private void TryInvokeAtStart()
    {
        if (_mainCarSpawner.CurrentCar == null) return;

        TryInvoke(_mainCarSpawner.CurrentCar);
    }
}
