using NaughtyAttributes;
public class ManualMonoEvent : MonoEvent
{
    [Button("Invoke")]
    public new void Invoke()
    {
        onMonoCall?.Invoke();
    }
}
