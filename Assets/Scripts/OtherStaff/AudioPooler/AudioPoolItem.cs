using System;
using DG.Tweening;
using UnityEngine;

[Serializable]
public class AudioPoolItem : MonoBehaviour
{
    public AudioSource audioSource;
    public TransformPositionLinker positionLinker;
    public Tween waitTween;
    public float priority;
    public int ID;

    #region MonoBehaviour

    private void OnDestroy()
    {
        waitTween.Kill();
    }

    #endregion
}
