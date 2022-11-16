using UnityEngine;

public class OnMoneyAddedMonoEvent : MonoEvent
{
    [Header("References")]
    [SerializeField] private Bank _bank;

    #region MonoBehaviour

    private void OnEnable()
    {
        _bank.onMoneyAdded += Invoke;
    }

    private void OnDisable()
    {
        _bank.onMoneyAdded -= Invoke;
    }

    #endregion

    private void Invoke(int money) => Invoke();
}
