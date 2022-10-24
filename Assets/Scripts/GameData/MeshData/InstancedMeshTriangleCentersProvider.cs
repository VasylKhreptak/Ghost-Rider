public class InstancedMeshTriangleCentersProvider : MeshTriangleCentersProvider
{
	protected override void UpdateData()
	{
		centers = _meshFilter.mesh.GetTriangleCenters();
		
		onMonoCall?.Invoke();
	}
}
