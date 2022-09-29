using TMPro;
using UnityEngine;
using Zenject;

public class MoneyText : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private TMP_Text _tmpText;

	private Bank _bank;
		
	[Inject]
	private void Construct(Bank bank)
	{
		_bank = bank;
	}

	#region MonoBehaviour

	private void OnValidate()
	{
		_tmpText ??= GetComponent<TMP_Text>();
	}

	private void Start()
	{
		UpdateValue(_bank.GetMoney());
	}

	private void OnEnable()
	{
		_bank.onMoneyChanged += UpdateValue;
	}

	private void OnDisable()
	{
		_bank.onMoneyChanged -= UpdateValue;
	}

	#endregion

	private void UpdateValue(int money)
	{
		_tmpText.text = money.ToString();
	}
}
