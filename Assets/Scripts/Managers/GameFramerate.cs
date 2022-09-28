using UnityEngine;

public class GameFramerate : MonoBehaviour
{
    public void Set(int framerate)
    {
        Application.targetFrameRate = framerate;
    }
}
