public class SharedMeshTriangleNormalsProvider : MeshTriangleNormalsProvider
{
	public override void UpdateData()
	{
		normals = _meshFilter.sharedMesh.GetTriangleNormals();
		
		onMonoCall?.Invoke();
	}
}
