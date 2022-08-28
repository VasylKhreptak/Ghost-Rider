public class OnDestroyEvent : MonoEvent
{
    #region MonoBehaviour

    private void OnDestroy()
    {
        onMonoCall?.Invoke();
    }

    #endregion
}
