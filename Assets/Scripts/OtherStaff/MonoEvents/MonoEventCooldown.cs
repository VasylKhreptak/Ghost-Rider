using DG.Tweening;
using UnityEngine;

public class MonoEventCooldown : MonoEvent
{
    [Header("Preferences")]
    [SerializeField, Min(0)] private float _coolDown;

    [Header("MonoEvent")]
    [SerializeField] private MonoEvent _event;

    private Tween _waitTween;

    private bool _canInvoke = true;
    
    #region MonoBehaviour

    private void OnValidate()
    {
        _event ??= GetComponent<MonoEvent>();
    }

    private void OnEnable()
    {
        _event.onMonoCall += TryInvoke;
    }

    private void OnDisable()
    {
        _event.onMonoCall -= TryInvoke;
    }

    private void OnDestroy()
    {
        _waitTween.Kill();
    }

    #endregion

    private void TryInvoke()
    {
        if (_canInvoke)
        {
            onMonoCall?.Invoke();

            ProcessCooldown();
        }
    }

    private void ProcessCooldown()
    {
        SetEventState(false);

        _waitTween = this.DOWait(_coolDown).OnComplete(() =>
        {
            SetEventState(true);
        });
    }

    private void SetEventState(bool enabled)
    {
        _canInvoke = enabled;
        _event.enabled = enabled;
    }

    public void CancelCooldown()
    {
        _waitTween.Kill();
    }
}
