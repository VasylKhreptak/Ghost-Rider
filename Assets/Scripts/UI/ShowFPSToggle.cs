using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ShowFPSToggle : UIUpdatableItem
{
	[Header("References")]
	[SerializeField] private Toggle _toggle;
	[SerializeField] private GameObject _FPSCounterObject;

	private SettingsProvider _settingsProvider;

	[Inject]
	private void Construct(SettingsProvider settingsProvider)
	{
		_settingsProvider = settingsProvider;
	}
	
	#region MonoBehaviour

	private void OnValidate()
	{
		_toggle ??= GetComponent<Toggle>();
	}
	
	private void Start()
	{
		UpdateValue();
	}

	private void OnEnable()
	{
		_toggle.onValueChanged.AddListener(OnValueChanged);
	}

	private void OnDisable()
	{
		_toggle.onValueChanged.RemoveListener(OnValueChanged);
	}

	#endregion

	public override void UpdateValue()
	{
		_toggle.isOn = _settingsProvider.settings.showFPS;
		_FPSCounterObject.SetActive(_toggle.isOn);
	}

	public void OnValueChanged(bool value)
	{
		_FPSCounterObject.SetActive(value);
		_settingsProvider.settings.showFPS = value;
	}
}
