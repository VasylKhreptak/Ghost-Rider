using System.Collections;

public class OnEnableEvent : MonoEvent
{
    #region MonoBehaviour

    private void OnEnable()
    {
        StartCoroutine(InvokeInNextFrame());
    }

    #endregion

    private IEnumerator InvokeInNextFrame()
    {
        yield return null;

        onMonoCall?.Invoke();
    }
}
