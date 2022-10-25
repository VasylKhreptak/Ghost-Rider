public class InstancedMeshTriangleCentersProvider : MeshTriangleCentersProvider
{
	protected override void SyncData()
	{
		centers = _meshFilter.mesh.GetTriangleCenters();
	}
}
