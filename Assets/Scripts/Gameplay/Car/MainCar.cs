using UnityEngine;

public class MainCar : MonoBehaviour
{
	[Header("Data")]
	public new Transform transform;
	public RCC_CarControllerV3 carController;
	public GameObject carObject;

	#region MonoBehaviour

	private void OnValidate()
	{
		transform ??= GetComponent<Transform>();
		carController ??= GetComponent<RCC_CarControllerV3>();
		carObject = gameObject;
	}

	#endregion

	public bool IsActive() => carObject.activeSelf;

	public void Disable() => carObject.SetActive(false);
}
