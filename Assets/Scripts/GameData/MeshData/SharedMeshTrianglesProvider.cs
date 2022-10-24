public class SharedMeshTrianglesProvider : MeshTrianglesProvider
{
	public override void UpdateData()
	{
		triangles = _meshFilter.sharedMesh.GetTriangles();
		
		onMonoCall?.Invoke();
	}
}
