public class InstancedMeshVerticesProvider : MeshVerticesProvider
{
	protected override void SyncData()
	{
		vertices = _meshFilter.mesh.vertices;
	}
}
