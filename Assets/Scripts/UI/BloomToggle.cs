using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class BloomToggle : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private Toggle _toggle;
	[SerializeField] private Volume _postProcessingVolume;

	private Bloom _bloom;
	
	#region MonoBehaviour

	private void OnValidate()
	{
		_toggle ??= GetComponent<Toggle>();
	}

	private void Awake()
	{
		_postProcessingVolume.profile.TryGet(out _bloom);
		_toggle.isOn = _bloom.IsActive();
	}

	private void OnEnable()
	{
		_toggle.onValueChanged.AddListener(SetBloomState);
	}

	private void OnDisable()
	{
		_toggle.onValueChanged.RemoveListener(SetBloomState);
	}

	#endregion

	private void SetBloomState(bool enabled)
	{
		_bloom.active = enabled;
	}
}
