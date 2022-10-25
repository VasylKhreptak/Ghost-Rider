public class MeshTriangleCentersProvider : MeshDataProviderCore
{
    public UnityEngine.Vector3[] centers;

    public UnityEngine.Vector3 GetWorldCenter(int index)
    {
        return _transform.TransformPoint(centers[index]);
    }
}
