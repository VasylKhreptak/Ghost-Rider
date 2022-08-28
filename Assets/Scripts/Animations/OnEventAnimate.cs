using System;
using UnityEngine;

public class OnEventAnimate : MonoEvent
{
    [Header("References")]
    [SerializeField] private AnimationCore _animation;
    [SerializeField] private MonoEvent _monoEvent;

    [Header("Preferences")]
    [SerializeField] private bool _forward = true;

    #region MonoBehavior

    private void OnValidate()
    {
        _animation ??= GetComponent<AnimationCore>();
        _monoEvent ??= GetComponent<MonoEvent>();
    }

    private void Awake()
    {
        _monoEvent.onMonoCall += Animate;
    }

    private void OnDestroy()
    {
        _monoEvent.onMonoCall -= Animate;
    }

    #endregion

    private void Animate()
    {
        _animation.Animate(_forward);
    }
}
