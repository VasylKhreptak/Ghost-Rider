public class MeshVerticesProvider : MeshDataProviderCore
{
	public UnityEngine.Vector3[] vertices;

	public UnityEngine.Vector3 GetWorldVertex(int index)
	{
		return _transform.TransformPoint(vertices[index]);
	}
}
