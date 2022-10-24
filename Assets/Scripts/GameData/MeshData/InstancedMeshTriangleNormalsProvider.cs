public class InstancedMeshTriangleNormalsProvider : MeshTriangleNormalsProvider
{
	protected override void UpdateData()
	{
		normals = _meshFilter.mesh.GetTriangleNormals();
		
		onMonoCall?.Invoke();
	}
}
