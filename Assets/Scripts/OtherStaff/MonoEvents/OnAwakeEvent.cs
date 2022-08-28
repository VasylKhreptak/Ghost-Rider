public class OnAwakeEvent : MonoEvent
{
    #region MonoBehaviour

    private void Awake()
    {
        onMonoCall?.Invoke();
    }

    #endregion
}
