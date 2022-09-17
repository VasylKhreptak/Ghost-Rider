using UnityEngine;

public class CursorStateController : MonoBehaviour
{
    public void SetState(bool enabled)
    {
        Cursor.visible = enabled;
        Cursor.lockState = enabled ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
