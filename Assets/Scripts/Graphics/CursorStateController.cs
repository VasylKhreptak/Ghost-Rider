using UnityEngine;

public class CursorStateController : MonoBehaviour
{
    public void SetState(bool enabled)
    {
        #if !UNITY_EDITOR
        Cursor.visible = enabled;
        Cursor.lockState = enabled ? CursorLockMode.None : CursorLockMode.Locked;
        #endif
    }
}
