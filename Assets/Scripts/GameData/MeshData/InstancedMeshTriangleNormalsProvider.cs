public class InstancedMeshTriangleNormalsProvider : MeshTriangleNormalsProvider
{
	protected override void SyncData()
	{
		normals = _meshFilter.mesh.GetTriangleNormals();
	}
}
