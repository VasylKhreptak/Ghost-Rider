using System;
using UnityEngine;

public class MonoEvent : MonoBehaviour
{
    public Action onMonoCall;

    public void Invoke()
    {
        onMonoCall?.Invoke();
    }
}
