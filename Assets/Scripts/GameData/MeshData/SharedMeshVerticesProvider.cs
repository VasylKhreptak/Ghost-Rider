public class SharedMeshVerticesProvider : MeshVerticesProvider
{
	protected override void SyncData()
	{
		vertices = _meshFilter.sharedMesh.vertices;
	}
}
