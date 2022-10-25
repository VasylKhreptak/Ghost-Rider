public class SharedMeshTriangleNormalsProvider : MeshTriangleNormalsProvider
{
	protected override void SyncData()
	{
		normals = _meshFilter.sharedMesh.GetTriangleNormals();
	}
}
