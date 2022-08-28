using UnityEngine;

public static class AnimationCurveExtensions
{
    public static float Evaluate(this AnimationCurve curve, Vector3 target, Vector3 source,
        float maxValue, float impactRadius)
    {
        return curve.Evaluate(Vector3.Distance(target, source) / impactRadius) * maxValue;
    }
}