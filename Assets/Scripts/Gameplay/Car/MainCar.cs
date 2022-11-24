using UnityEngine;

public class MainCar : MonoBehaviour
{
	[Header("Data")]
	[SerializeField] private Transform _transform;
	[SerializeField] private  RCC_CarControllerV3 _carController;
	[SerializeField] private  GameObject _carObject;
	[SerializeField] private  DamageableObject _damageableObject;
	[SerializeField] private Pools _pool;

	public Transform Transform => _transform;
	public RCC_CarControllerV3 CarController => _carController;
	public GameObject GameObject => _carObject;
	public DamageableObject DamageableObject => _damageableObject;
	public Pools Pool => _pool;
	
	#region MonoBehaviour

	private void OnValidate()
	{
		_transform ??= GetComponent<Transform>();
		_carController ??= GetComponent<RCC_CarControllerV3>();
		_damageableObject ??= GetComponent<DamageableObject>();
		_carObject = gameObject;
	}

	#endregion

	public bool IsActive() => _carObject.activeSelf;

	public void Disable() => _carObject.SetActive(false);
}
