public class InstancedMeshTrianglesProvider : MeshTrianglesProvider
{
	protected override void SyncData()
	{
		triangles = _meshFilter.mesh.GetTriangles();
	}
}
