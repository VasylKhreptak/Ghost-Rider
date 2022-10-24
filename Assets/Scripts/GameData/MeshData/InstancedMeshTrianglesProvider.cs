public class InstancedMeshTrianglesProvider : MeshTrianglesProvider
{
	public override void UpdateData()
	{
		triangles = _meshFilter.mesh.GetTriangles();
		
		onMonoCall?.Invoke();
	}
}
