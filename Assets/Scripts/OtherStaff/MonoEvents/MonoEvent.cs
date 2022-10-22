using System;
using UnityEngine;

public class MonoEvent : MonoBehaviour
{
    public Action onMonoCall;

    public virtual void Invoke()
    {
        onMonoCall?.Invoke();
    }
}
