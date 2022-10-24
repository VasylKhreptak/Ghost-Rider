public class InstancedMeshVerticesProvider : MeshVerticesProvider
{
	protected override void UpdateData()
	{
		vertices = _meshFilter.mesh.vertices;
		
		onMonoCall?.Invoke();
	}
}
