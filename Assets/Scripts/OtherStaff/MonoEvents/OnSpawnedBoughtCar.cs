using UnityEngine;

public class OnSpawnedBoughtCar : MonoEvent
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
        _affordanceEvents.onSpawnedBough += Invoke;
    }

    private void OnDisable()
    {
        _affordanceEvents.onSpawnedBough -= Invoke;
    }

    #endregion
}
