using UnityEngine;

public static class Vector3Extensions
{
    public static Vector3 FindClosest(this Vector3 origin, Vector3[] positions)
    {
        Vector3 closestPosition = positions[0];
        float closestDistanceSqr = Mathf.Infinity;

        foreach (var potentialPosition in positions)
        {
            Vector3 directionToTarget = potentialPosition - origin;
            float sqrDirectionToTarget = directionToTarget.sqrMagnitude;

            if (sqrDirectionToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = sqrDirectionToTarget;
                closestPosition = potentialPosition;
            }
        }

        return closestPosition;
    }
}