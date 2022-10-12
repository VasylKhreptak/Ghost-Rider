namespace Extensions
{
    public static class Random
    {
        public static int Sign()
        {
            return UnityEngine.Random.value < 0.5f ? 1 : -1;
        }

        public static bool Boolean()
        {
            return UnityEngine.Random.value < 0.5f;
        }

        public static UnityEngine.Vector3 Range(UnityEngine.Vector3 min, UnityEngine.Vector3 max)
        {
            return new UnityEngine.Vector3(UnityEngine.Random.Range(min.x, max.x), UnityEngine.Random.Range(min.y, max.y), 
                UnityEngine.Random.Range(min.z, max.z));
        }
    }
}
