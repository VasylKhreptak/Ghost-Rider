public class SharedMeshTriangleCentersProvider : MeshTriangleCentersProvider
{
	protected override void UpdateData()
	{
		centers = _meshFilter.sharedMesh.GetTriangleCenters();
		
		onMonoCall?.Invoke();
	}
}
