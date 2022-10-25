public class MeshTriangleNormalsProvider : MeshDataProviderCore
{
	public UnityEngine.Vector3[] normals;

	public UnityEngine.Vector3 GetWorldNormal(int index)
	{
		return _transform.TransformDirection(normals[index]);
	}
}
