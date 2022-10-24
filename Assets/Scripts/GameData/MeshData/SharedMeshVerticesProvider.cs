public class SharedMeshVerticesProvider : MeshVerticesProvider
{
	public override void UpdateData()
	{
		vertices = _meshFilter.sharedMesh.vertices;
		
		onMonoCall?.Invoke();
	}
}
