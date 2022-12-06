using DG.Tweening;
using UnityEngine;

public class ConnectionWarningSignAnimation : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private RectTransform _rectTransform;

	[Header("Preferences")]
	[SerializeField] private float _showMoveDuration;
	[SerializeField] private AnimationCurve _showMoveCurve;
	[SerializeField] private float _showDuration;
	[SerializeField] private float _hideMoveDuration;
	[SerializeField] private AnimationCurve _hideMoveCurve;

	[Header("Events")]
	[SerializeField] private MonoEvent _onConnectedEvent;
	[SerializeField] private MonoEvent _onDisconnectedEvent;
	[SerializeField] private MonoEvent _connectionChangedEvent;

	private Tween _showMoveTween;
	private Tween _showTween;
	private Tween _hideMoveTween;

	#region MonoBehaviour



	#endregion
}
