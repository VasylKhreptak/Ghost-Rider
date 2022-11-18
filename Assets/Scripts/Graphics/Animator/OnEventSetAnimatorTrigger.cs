using UnityEngine;

public class OnEventSetAnimatorTrigger : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private Animator _animator;

	[Header("Preferences")]
	[SerializeField] private string _triggerName;

	[Header("Events")]
	[SerializeField] private MonoEvent _triggerEvent;
	
	#region MonoBehaviour

	private void OnValidate()
	{
		_animator ??= GetComponent<Animator>();
	}

	private void OnEnable()
	{
		_triggerEvent.onMonoCall += SetTrigger;
	}

	private void OnDisable()
	{
		_triggerEvent.onMonoCall -= SetTrigger;
	}

	#endregion

	private void SetTrigger()
	{
		_animator.SetTrigger(_triggerName);
	}
}
