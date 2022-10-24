public class SharedMeshTrianglesProvider : MeshTrianglesProvider
{
	protected override void UpdateTriangles()
	{
		triangles = _meshFilter.sharedMesh.GetTriangles();
		
		onMonoCall?.Invoke();
	}
}
