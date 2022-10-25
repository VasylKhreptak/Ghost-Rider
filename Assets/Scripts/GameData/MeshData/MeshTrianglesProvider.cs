public class MeshTrianglesProvider : MeshDataProviderCore
{
	public MeshTriangle[] triangles;

	public MeshTriangle GetWorldTriangle(int index)
	{
		MeshTriangle triangle = new MeshTriangle
		{
			p1 = _transform.TransformPoint(triangles[index].p1),
			p2 = _transform.TransformPoint(triangles[index].p2),
			p3 = _transform.TransformPoint(triangles[index].p3),
			center = _transform.TransformPoint(triangles[index].center),
			normal = _transform.TransformDirection(triangles[index].normal)
		};

		return triangle;
	}
}


