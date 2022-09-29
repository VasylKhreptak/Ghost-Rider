using System;
using UnityEngine;
using Zenject;

public class Bank : MonoBehaviour
{
    private PlayerDataProvider _playerDataProvider;

    public Action<int> onMoneyChanged;
    
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
        _playerDataProvider.playerData.money = Mathf.Clamp(money, 0, int.MaxValue);
        
        onMoneyChanged?.Invoke(_playerDataProvider.playerData.money);
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
        return _playerDataProvider.playerData.money >= money;
    }
    
    private void SpendMoney(int money)
    {
        _playerDataProvider.playerData.money -= money;
        
        onMoneyChanged?.Invoke(_playerDataProvider.playerData.money);
    }
}
