using UnityEngine;

public static class AnimationCurveExtensions
{
    public static float Evaluate(this AnimationCurve curve, Vector3 target, Vector3 source,
        float maxValue, float impactRadius)
    {
        return curve.Evaluate(Vector3.Distance(target, source) / impactRadius) * maxValue;
    }

    /// <summary>
    /// If i <= 0, returns min
    /// If i >= 1, returns max
    /// </summary>
    /// <param name="curve"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <param name="i"></param>
    /// <returns></returns>
    public static float Evaluate(this AnimationCurve curve, float min, float max, float i)
    {
        return curve.Evaluate(i) * (max - min) + min;
    }
    
    /// <summary>
    /// If x == minX, returns minY
    /// if x == maxX, returns maxY
    /// Other values of x is evaluating
    /// </summary>
    /// <returns></returns>
    public static float Evaluate(this AnimationCurve curve, float minX, float maxX, float x, float minY, float maxY)
    {
        float remappedX = x.Remap(minX, maxX, 0f, 1f);

        return curve.Evaluate(minY, maxY, remappedX);
    }
}
