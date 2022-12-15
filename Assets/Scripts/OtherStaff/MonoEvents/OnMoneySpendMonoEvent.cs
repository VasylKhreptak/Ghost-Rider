using UnityEngine;

public class OnMoneySpendMonoEvent : MonoEvent
{
    [Header("References")]
    [SerializeField] private Bank _bank;

    #region MonoBehaviour

    private void OnValidate()
    {
        _bank ??= FindObjectOfType<Bank>();
    }

    private void OnEnable()
    {
        _bank.onMoneySpend += Invoke;
    }

    private void OnDisable()
    {
        _bank.onMoneySpend -= Invoke;
    }

    #endregion

    private void Invoke(int money) => Invoke();
}
