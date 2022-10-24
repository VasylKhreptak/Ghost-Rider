public class InstancedMeshVerticesProvider : MeshVerticesProvider
{
	protected override void UpdateVertices()
	{
		vertices = _meshFilter.mesh.vertices;
		
		onMonoCall?.Invoke();
	}
}
