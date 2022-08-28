public class ManualMonoEvent : MonoEvent
{
    public void Invoke()
    {
        onMonoCall?.Invoke();
    }
}
