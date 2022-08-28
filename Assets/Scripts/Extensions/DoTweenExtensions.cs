using UnityEngine;

namespace DG.Tweening
{
    public static class DoTweenExtensions
    {
        public static Tween DOWait(this MonoBehaviour owner, float duration)
        {
            return DOTween.To(() => 0, (curProgress) => { }, 1f, duration);
        }
    }
}
