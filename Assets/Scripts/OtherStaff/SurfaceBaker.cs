using UnityEngine;
using UnityEngine.AI;

public class SurfaceBaker : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private NavMeshSurface _navMeshSurface;

	#region MonoBehaviour

	private void OnValidate()
	{
		_navMeshSurface ??= GetComponent<NavMeshSurface>();
	}

	#endregion

	public void Bake()
	{
		_navMeshSurface.UpdateNavMesh(_navMeshSurface.navMeshData);
	}
}
