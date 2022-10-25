public class SharedMeshTrianglesProvider : MeshTrianglesProvider
{
	protected override void SyncData()
	{
		triangles = _meshFilter.sharedMesh.GetTriangles();
	}
}
