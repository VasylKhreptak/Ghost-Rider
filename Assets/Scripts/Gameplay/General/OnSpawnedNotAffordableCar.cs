using UnityEngine;

public class OnSpawnedNotAffordableCar : MonoEvent
{
   [Header("References")]
   [SerializeField] private ShopCarAffordanceEvents _affordanceEvents;

   #region MonoBehaviour

   private void OnValidate()
   {
      _affordanceEvents ??= GetComponent<ShopCarAffordanceEvents>();
   }

   private void OnEnable()
   {
      _affordanceEvents.onSpawnedNotAffordable += Invoke;
   }

   private void OnDisable()
   {
      _affordanceEvents.onSpawnedNotAffordable -= Invoke;
   }

   #endregion
}
