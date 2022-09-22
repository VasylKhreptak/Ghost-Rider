using UnityEngine;

public class InvokeMonoEvent : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MonoEvent _monoEvent;

    #region MonoBehaviour

    private void OnValidate()
    {
        _monoEvent ??= GetComponent<MonoEvent>();
    }

    #endregion

    public void Invoke()
    {
        _monoEvent.onMonoCall.Invoke();
    }
}
