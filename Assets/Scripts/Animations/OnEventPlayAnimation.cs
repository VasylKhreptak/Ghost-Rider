using UnityEngine;

public class OnEventPlayAnimation : MonoBehaviour
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
        _monoEvent.onMonoCall += PlayAnimation;
    }

    private void OnDestroy()
    {
        _monoEvent.onMonoCall -= PlayAnimation;
    }

    #endregion

    private void PlayAnimation()
    {
        _animationCore.Play();
    }
}
