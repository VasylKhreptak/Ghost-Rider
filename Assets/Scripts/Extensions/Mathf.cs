namespace Extensions
{
    public class Mathf
    {
        public static bool Approximately(float a, float b, float precision)
        {
            return UnityEngine.Mathf.Abs(a - b) < precision;
        }

        public static int Sign(bool value)
        {
            return value ? 1 : -1;
        }
    }
}
