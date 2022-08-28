public class OnDisableEvent : MonoEvent
{
    #region MonoBehaviour

    private void OnDisable()
    {
        onMonoCall?.Invoke();
    }

    #endregion
}
