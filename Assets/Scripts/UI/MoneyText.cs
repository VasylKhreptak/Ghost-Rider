using UnityEngine;
using Zenject;

public class MoneyText : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private TMP_SmoothIntText _tmpText;

	private Bank _bank;
		
	[Inject]
	private void Construct(Bank bank)
	{
		_bank = bank;
	}

	#region MonoBehaviour

	private void OnValidate()
	{
		_tmpText ??= GetComponent<TMP_SmoothIntText>();
	}

	private void Start()
	{
		UpdateValue(_bank.GetMoney());
	}

	private void OnEnable()
	{
		_bank.onMoneyUpdated += UpdateValue;
	}

	private void OnDisable()
	{
		_bank.onMoneyUpdated -= UpdateValue;
	}

	#endregion

	private void UpdateValue(int money)
	{
		_tmpText.text = money.ToString();
	}
}
