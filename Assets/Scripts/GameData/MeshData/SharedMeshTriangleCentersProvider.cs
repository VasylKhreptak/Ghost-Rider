public class SharedMeshTriangleCentersProvider : MeshTriangleCentersProvider
{
	protected override void SyncData()
	{
		centers = _meshFilter.sharedMesh.GetTriangleCenters();
	}
}
