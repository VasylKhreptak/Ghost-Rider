using UnityEngine;

public class OnSpawnedAffordableCar : MonoEvent
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
        _affordanceEvents.onSpawnedAffordable += Invoke;
    }

    private void OnDisable()
    {
        _affordanceEvents.onSpawnedAffordable -= Invoke;
    }

    #endregion
}
