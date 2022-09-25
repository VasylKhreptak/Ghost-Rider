using System;
using TMPro;
using UnityEngine;

public class TextureResolutionDropdown : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private TMP_Dropdown _dropdown;

	#region MonoBehaviour

	private void OnValidate()
	{
		_dropdown ??= GetComponent<TMP_Dropdown>();
	}

	private void Awake()
	{
		_dropdown.value = QualitySettings.masterTextureLimit;
	}

	private void OnEnable()
	{
		_dropdown.onValueChanged.AddListener(SetResolution);
	}

	private void OnDisable()
	{
		_dropdown.onValueChanged.RemoveListener(SetResolution);
	}

	#endregion

	private void SetResolution(int index)
	{
		QualitySettings.masterTextureLimit = index;
	}
}
