using UnityEngine;

public abstract class CameraProvider: MonoBehaviour
{
	public abstract Camera Camera { get; protected set; }
}
