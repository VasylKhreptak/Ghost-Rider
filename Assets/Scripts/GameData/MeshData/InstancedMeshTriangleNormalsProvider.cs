public class InstancedMeshTriangleNormalsProvider : MeshTriangleNormalsProvider
{
	public override void UpdateData()
	{
		normals = _meshFilter.mesh.GetTriangleNormals();
		
		onMonoCall?.Invoke();
	}
}
