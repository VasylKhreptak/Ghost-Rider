public class SharedMeshTriangleNormalsProvider : MeshTriangleNormalsProvider
{
	protected override void UpdateData()
	{
		normals = _meshFilter.sharedMesh.GetTriangleNormals();
		
		onMonoCall?.Invoke();
	}
}
