using TMPro;
using UnityEngine;

public class GearCounter : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private TMP_Text _tmp;
	[SerializeField] private MainCarSpawner _mainCarSpawner;

	#region MonoBehaviour

	private void OnValidate()
	{
		_tmp ??= GetComponent<TMP_Text>();
	}

	private void Awake()
	{
		_mainCarSpawner.onSpawn += OnSpawn;
		_mainCarSpawner.onDespawn += OnDespawn;
	}

	private void OnDestroy()
	{
		_mainCarSpawner.onSpawn -= OnSpawn;
		_mainCarSpawner.onDespawn -= OnDespawn;
		
		RemoveUpdateListeners();
	}

	#endregion

	private void OnSpawn(MainCar mainCar)
	{
		AddUpdateListeners();
	}

	private void OnDespawn(MainCar mainCar)
	{
		RemoveUpdateListeners();
	}
	
	private void AddUpdateListeners()
	{
		_mainCarSpawner.CurrentCar.CarController.onGearShifted += UpdateValue;
	}

	private void RemoveUpdateListeners()
	{
		_mainCarSpawner.CurrentCar.CarController.onGearShifted -= UpdateValue;
	}
	
	private void UpdateValue(int gear)
	{
		_tmp.text = gear.ToString();
	}
}

