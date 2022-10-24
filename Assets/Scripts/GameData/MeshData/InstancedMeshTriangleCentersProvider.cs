public class InstancedMeshTriangleCentersProvider : MeshTriangleCentersProvider
{
	public override void UpdateData()
	{
		centers = _meshFilter.mesh.GetTriangleCenters();
		
		onMonoCall?.Invoke();
	}
}
