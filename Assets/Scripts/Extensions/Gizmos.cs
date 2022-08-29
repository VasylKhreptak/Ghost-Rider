using UnityEngine;

namespace Extensions
{
    public static class Gizmos
    {
        public static void DrawArrow(Vector3 position, Vector3 direction, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
        {
            UnityEngine.Gizmos.DrawRay(position, direction);
       
            Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0,180+arrowHeadAngle,0) * new Vector3(0,0,1);
            Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0,180-arrowHeadAngle,0) * new Vector3(0,0,1);
            UnityEngine.Gizmos.DrawRay(position + direction, right * arrowHeadLength);
            UnityEngine.Gizmos.DrawRay(position + direction, left * arrowHeadLength);
        }
    }
}
