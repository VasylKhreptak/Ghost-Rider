using UnityEngine;

public class OnEventPauseAnimation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AnimationCore _animationCore;
    [SerializeField] private MonoEvent _monoEvent;
    
    #region MonoBehaviour

    private void OnValidate()
    {
        _animationCore ??= GetComponent<AnimationCore>();
        _monoEvent ??= GetComponent<MonoEvent>();
    }

    private void Awake()
    {
        _monoEvent.onMonoCall += PauseAnimation;
    }

    private void OnDestroy()
    {
        _monoEvent.onMonoCall -= PauseAnimation;
    }

    #endregion

    private void PauseAnimation()
    {
        _animationCore.Pause();
    }
}
