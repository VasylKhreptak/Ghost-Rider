public class InstancedMeshTrianglesProvider : MeshTrianglesProvider
{
	protected override void UpdateTriangles()
	{
		triangles = _meshFilter.mesh.GetTriangles();
		
		onMonoCall?.Invoke();
	}
}
