public class SharedMeshTriangleCentersProvider : MeshTriangleCentersProvider
{
	public override void UpdateData()
	{
		centers = _meshFilter.sharedMesh.GetTriangleCenters();
		
		onMonoCall?.Invoke();
	}
}
