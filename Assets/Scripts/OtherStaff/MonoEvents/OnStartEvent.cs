public class OnStartEvent : MonoEvent
{
    #region MonoBehaviour

    private void Start()
    {
        onMonoCall?.Invoke();
    }

    #endregion
}
