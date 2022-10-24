public class SharedMeshVerticesProvider : MeshVerticesProvider
{
	protected override void UpdateData()
	{
		vertices = _meshFilter.sharedMesh.vertices;
		
		onMonoCall?.Invoke();
	}
}
