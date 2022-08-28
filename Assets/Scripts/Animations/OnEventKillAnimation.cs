using UnityEngine;

public class OnEventKillAnimation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MonoEvent _monoEvent;
    [SerializeField] private AnimationCore _animation;

    #region MonoBehaviour

    private void OnValidate()
    {
        _monoEvent ??= GetComponent<MonoEvent>();
        _animation ??= GetComponent<AnimationCore>();
    }

    private void Awake()
    {
        _monoEvent.onMonoCall += Kill;
    }

    private void OnDestroy()
    {
        _monoEvent.onMonoCall -= Kill;
    }

    #endregion

    private void Kill()
    {
        _animation.Kill();
    }
}
