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
    }
}
