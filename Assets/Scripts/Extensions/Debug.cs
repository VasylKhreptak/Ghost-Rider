using UnityEngine;

namespace Extensions
{
    public static class Debug
    {
        public static void DrawArrow(Vector3 pos, Vector3 direction, Color color, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
        {
            UnityEngine.Debug.DrawRay(pos, direction);

            Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            UnityEngine.Debug.DrawRay(pos + direction, right * arrowHeadLength, color);
            UnityEngine.Debug.DrawRay(pos + direction, left * arrowHeadLength, color);
        }
    }
}
