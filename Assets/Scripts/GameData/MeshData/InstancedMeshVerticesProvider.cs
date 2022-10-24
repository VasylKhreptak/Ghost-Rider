public class InstancedMeshVerticesProvider : MeshVerticesProvider
{
	public override void UpdateData()
	{
		vertices = _meshFilter.mesh.vertices;
		
		onMonoCall?.Invoke();
	}
}
