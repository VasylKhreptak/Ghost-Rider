public class ManualMonoEvent : MonoEvent
{
    public new void Invoke()
    {
        onMonoCall?.Invoke();
    }
}
