using System;
using System.Linq;
using System.Security.Cryptography;
using ModestTree;
using UnityEngine;

public class MeshClosestTriangleFinder : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private Transform _transform;
	[SerializeField] private MeshFilter _targetMeshFilter;
	[SerializeField] private Transform _targetTransform;

	[Header("Result")]
	[SerializeField] private int _triangleIndex;

	#region MonoBehaviour

	private void OnValidate()
	{
		_transform ??= GetComponent<Transform>();

		if (_targetMeshFilter == null) return;

		_targetTransform = _targetMeshFilter.transform;
	}

	#endregion

	private void OnDrawGizmosSelected()
	{
		if (_transform == null || _targetTransform == null || _targetMeshFilter == null) return;

		Vector3[] centers = _targetMeshFilter.sharedMesh.GetTriangleCenters();
		Vector3[] normals = _targetMeshFilter.sharedMesh.GetTriangleNormals();

		Vector3[] worldCenters = new Vector3[centers.Length];
		Vector3[] worldNormals = new Vector3[normals.Length];
		
		for (int i = 0; i < worldCenters.Length; i++)
		{
			worldCenters[i] = _targetTransform.TransformPoint(centers[i]);
			worldNormals[i] = _targetTransform.TransformDirection(normals[i]);
		}

		Vector3 closestCenter = _transform.position.FindClosest(worldCenters);

		_triangleIndex = worldCenters.IndexOf(closestCenter);
		
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(closestCenter, 0.02f);
		Gizmos.DrawRay(closestCenter, worldNormals[_triangleIndex]);
	}
}
