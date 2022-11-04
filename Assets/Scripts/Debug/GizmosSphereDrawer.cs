using UnityEngine;

public class GizmosSphereDrawer : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private Transform _transform;

	[Header("Preferences")]
	[SerializeField] private Vector3 _offset;
	[SerializeField] private float _radius;
	[SerializeField] private Color _color;
	
	#region MonoBehaviour

	private void OnValidate()
	{
		_transform ??= GetComponent<Transform>();
	}

	#endregion

	private void OnDrawGizmos()
	{
		if (_transform == null) return;

		Gizmos.color = _color;
		Gizmos.DrawSphere(_transform.position + _offset, _radius);
	}
}
