using UnityEngine;

public class CursorStateController : MonoBehaviour
{
    public void SetVisible(bool visible)
    {
#if !UNITY_EDITOR
        Cursor.visible = visible;
#endif
    }

    public void SetLock(bool @lock)
    {
#if !UNITY_EDITOR
        Cursor.lockState = @lock ? CursorLockMode.Locked : CursorLockMode.None;
#endif
    }
}
