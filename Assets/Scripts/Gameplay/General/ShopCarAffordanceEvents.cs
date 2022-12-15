using System;
using UnityEngine;

public class ShopCarAffordanceEvents : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerDataProvider _playerDataProvider;
    [SerializeField] private MainCarSpawner _mainCarSpawner;
    [SerializeField] private Bank _bank;

    [Header("Events")]
    [SerializeField] private MonoEvent _checkEvent;

    public Action onSpawnedAffordable;
    public Action onSpawnedNotAffordable;
    public Action onSpawnedBough;
    public Action onSpawnedNotBought;
    
    #region MonoBehaviour

    private void OnValidate()
    {
        _mainCarSpawner ??= FindObjectOfType<MainCarSpawner>();
        _playerDataProvider ??= FindObjectOfType<PlayerDataProvider>();
        _bank ??= FindObjectOfType<Bank>();
    }

    private void OnEnable()
    {
        _checkEvent.onMonoCall += TryInvoke;
    }

    private void OnDisable()
    {
        _checkEvent.onMonoCall -= TryInvoke;
    }

    #endregion

    private void TryInvoke()
    {
        if (_mainCarSpawner.CurrentCar == null) return;
        
        bool isAffordable = CanAfford(_mainCarSpawner.CurrentCar);
        bool isCarBought = IsCarBought(_mainCarSpawner.CurrentCar);

        (isCarBought ? onSpawnedBough : onSpawnedNotBought)?.Invoke();
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
}
