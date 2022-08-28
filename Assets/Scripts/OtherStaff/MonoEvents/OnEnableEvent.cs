public class OnEnableEvent : MonoEvent
{
    #region MonoBehaviour

    private void OnEnable()
    {
        onMonoCall?.Invoke();
    }

    #endregion
}
