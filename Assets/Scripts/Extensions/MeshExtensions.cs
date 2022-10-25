using UnityEngine;

public static class MeshExtensions
{
    public static Vector3[] GetTriangleCenters(this Mesh mesh)
    {
        int[] triangles = mesh.triangles;
        Vector3[] vertices = mesh.vertices;
        int centersCount = triangles.Length / 3;

        Vector3[] triangleCenters = new Vector3[centersCount];

        int counter = 0;
        for (int i = 0; i < triangles.Length;)
        {
            Vector3 p1 = vertices[triangles[i++]];
            Vector3 p2 = vertices[triangles[i++]];
            Vector3 p3 = vertices[triangles[i++]];

            Vector3 center = (p1 + p2 + p3) / 3;

            triangleCenters[counter] = center;

            counter++;
        }

        return triangleCenters;
    }

    public static Vector3[] GetTriangleNormals(this Mesh mesh)
    {
        int[] triangles = mesh.triangles;
        Vector3[] vertices = mesh.vertices;
        int centersCount = triangles.Length / 3;

        Vector3[] triangleNormals = new Vector3[centersCount];

        int counter = 0;
        for (int i = 0; i < triangles.Length;)
        {
            Vector3 p1 = vertices[triangles[i++]];
            Vector3 p2 = vertices[triangles[i++]];
            Vector3 p3 = vertices[triangles[i++]];

            Vector3 normal = Vector3.Cross( p2 - p1, p3 - p1).normalized;

            triangleNormals[counter] = normal;

            counter++;
        }

        return triangleNormals;
    }

    public static MeshTriangle[] GetTriangles(this Mesh mesh)
    {
        int[] triangles = mesh.triangles;
        Vector3[] vertices = mesh.vertices;
        Vector3[] normals = mesh.normals;
        int centersCount = triangles.Length / 3;
        MeshTriangle[] meshTriangles = new MeshTriangle[centersCount];

        int triangleIndex = 0;
        for (int i = 0; i < triangles.Length;)
        {
            int i1 = i++;
            int i2 = i++;
            int i3 = i++;

            Vector3 p1 = vertices[triangles[i1]];
            Vector3 p2 = vertices[triangles[i2]];
            Vector3 p3 = vertices[triangles[i3]];
            Vector3 center = (p1 + p2 + p3) / 3;

            Vector3 normal1 = normals[triangles[i1]];
            Vector3 normal2 = normals[triangles[i2]];
            Vector3 normal3 = normals[triangles[i3]];
            Vector3 normal = (normal1 + normal2 + normal3) / 3;

            meshTriangles[triangleIndex] = new MeshTriangle
            {
                p1 = p1,
                p2 = p2,
                p3 = p3,
                center = center,
                normal = normal
            };

            triangleIndex++;
        }

        return meshTriangles;
    }
}

[System.Serializable]
public class MeshTriangle
{
    public Vector3 p1;
    public Vector3 p2;
    public Vector3 p3;
    public Vector3 center;
    public Vector3 normal;
}
