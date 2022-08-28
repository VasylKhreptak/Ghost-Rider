using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;

public static class TransformExtensions
{
    public static Transform FindClosestTransform(this Transform transform, Transform[] transforms)
    {
        Transform closestTransform = null;
        var closestDistanceSqr = Mathf.Infinity;

        foreach (var potentialTransform in transforms)
        {
            var directionToTarget = potentialTransform.position - transform.position;
            var sqrDirectionToTarget = directionToTarget.sqrMagnitude;

            if (sqrDirectionToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = sqrDirectionToTarget;
                closestTransform = potentialTransform;
            }
        }

        return closestTransform;
    }

    public static Transform[] GetChildren(this Transform transform)
    {
        List<Transform> children = new List<Transform>();

        foreach (Transform child in transform)
        {
            children.Add(child);
        }

        return children.ToArray();
    }

    public static void Reset(this Transform transform)
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }

    public static void ResetPosition(this Transform transform)
    {
        transform.position = Vector3.zero;
    }

    public static void ResetRotation(this Transform transform)
    {
        transform.rotation = Quaternion.identity;
    }

    public static void ResetScale(this Transform transform)
    {
        transform.localScale = Vector3.one;
    }
}
