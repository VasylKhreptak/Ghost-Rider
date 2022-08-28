using UnityEngine;

public abstract class AnimationCore : MonoBehaviour
{
    public abstract void Animate(bool state);

    public abstract void Pause();

    public abstract void Play();
    
    public abstract void Kill();
}