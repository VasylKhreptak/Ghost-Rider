using UnityEngine;
using UnityEngine.UI;

public class CarBuyButton : MonoEvent
{
   [Header("References")]
   [SerializeField] private Button _button;
   [SerializeField] private PlayerDataProvider _playerDataProvider;
   [SerializeField] private Bank _bank;
   [SerializeField] private MainCarSpawner _mainCarSpawner;

   #region MonoBehaviour

   private void OnValidate()
   {
      _button ??= GetComponent<Button>();
      _playerDataProvider ??= FindObjectOfType<PlayerDataProvider>();
      _bank ??= FindObjectOfType<Bank>();
   }

   private void OnEnable()
   {
      _button.onClick.AddListener(TryBuyCar);
   }

   private void OnDisable()
   {
      _button.onClick.RemoveListener(TryBuyCar);
   }

   #endregion

   private void TryBuyCar()
   {
      if (_bank.TrySpendMoney(_mainCarSpawner.CurrentCar.CarSpecification.Price))
      {
         BuyCar();
      }
   }

   private void BuyCar()
   {
      _playerDataProvider.playerData.boughtCars.Add(_mainCarSpawner.CurrentCar.Pool);
      _playerDataProvider.playerData.lastCar = _mainCarSpawner.CurrentCar.Pool;
      
      onMonoCall?.Invoke();
   }
}
