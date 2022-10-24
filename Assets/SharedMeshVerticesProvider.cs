public class SharedMeshVerticesProvider : MeshVerticesProvider
{
	protected override void UpdateVertices()
	{
		vertices = _meshFilter.sharedMesh.vertices;
		
		onMonoCall?.Invoke();
	}
}
