using UnityEngine;

public class AnimationGroup : AnimationCore
{
    [Header("References")]
    [SerializeField] private AnimationCore[] _animations;

    public override void Animate(bool state)
    {
        foreach (var animation in _animations)
        {
            animation.Animate(state);
        }
    }
    public override void Pause()
    {
        foreach (var animation in _animations)
        {
            animation.Pause();
        }
    }
    public override void Play()
    {
        foreach (var animation in _animations)
        {
            animation.Play();
        }
    }
    public override void Kill()
    {
        foreach (var animation in _animations)
        {
            animation.Kill();
        }
    }
}
