public class InstancedMeshTrianglesProvider : MeshTrianglesProvider
{
	protected override void UpdateData()
	{
		triangles = _meshFilter.mesh.GetTriangles();
		
		onMonoCall?.Invoke();
	}
}
