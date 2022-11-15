using System;
using UnityEngine;
using Zenject;

public class Bank : MonoBehaviour
{
    private PlayerDataProvider _playerDataProvider;

    public bool IsEmpty => _playerDataProvider.playerData.money == 0;
    
    public Action<int> onMoneyUpdated;
    public Action<int> onMoneyAdded;
    public Action<int> onMoneySpend;

    [Inject]
    private void Construct(PlayerDataProvider playerDataProvider)
    {
        _playerDataProvider = playerDataProvider;
    }

    public int GetMoney()
    {
        return _playerDataProvider.playerData.money;
    }

    public void SetMoney(int money)
    {
        _playerDataProvider.playerData.money = GetClamped(money);
        
        onMoneyUpdated?.Invoke(_playerDataProvider.playerData.money);
    }

    public void AddMoney(int money)
    {
        money = GetClamped(money);
        
        SetMoney(_playerDataProvider.playerData.money + money);
        
        onMoneyAdded?.Invoke(money);
    }
    
    public bool TrySpendMoney(int money)
    {
        if (CanSpendMoney(money))
        {
            SpendMoney(money);

            return true;
        }

        return false;
    }

    private bool CanSpendMoney(int money)
    {
        return _playerDataProvider.playerData.money >= money && money > 0;
    }
    
    private void SpendMoney(int money)
    {
        _playerDataProvider.playerData.money -= money;
        
        onMoneySpend?.Invoke(money);
        
        onMoneyUpdated?.Invoke(_playerDataProvider.playerData.money);
    }

    private int GetClamped(int money)
    {
        return Mathf.Clamp(money, 0, int.MaxValue);
    }
}
