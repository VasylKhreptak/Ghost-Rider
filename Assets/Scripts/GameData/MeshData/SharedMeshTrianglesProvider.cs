public class SharedMeshTrianglesProvider : MeshTrianglesProvider
{
	protected override void UpdateData()
	{
		triangles = _meshFilter.sharedMesh.GetTriangles();
		
		onMonoCall?.Invoke();
	}
}
