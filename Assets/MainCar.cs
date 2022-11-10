using System;
using UnityEngine;

public class MainCar : MonoBehaviour
{
	[Header("Data")]
	public RCC_CarControllerV3 carController;
	[SerializeField] private int _id;

	public int ID => _id;

	#region MonoBehaviour

	private void OnValidate()
	{
		carController ??= GetComponent<RCC_CarControllerV3>();
	}

	#endregion
}
