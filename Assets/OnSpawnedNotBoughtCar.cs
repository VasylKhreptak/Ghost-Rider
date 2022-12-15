using UnityEngine;

public class OnSpawnedNotBoughtCar : MonoEvent
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
        _affordanceEvents.onSpawnedNotBought += Invoke;
    }

    private void OnDisable()
    {
        _affordanceEvents.onSpawnedNotBought -= Invoke;
    }

    #endregion
}
