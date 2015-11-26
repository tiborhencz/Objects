using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TessalateToMusic : AnimateToMusic
{

	Vector3[] m_TetrahedronVertices;
	int[] m_TetrahedronIndices;
	MeshFilter m_MeshFilter;

	protected override void Setup ()
	{
		float height = Mathf.Sqrt(0.5f) / 2f;
		m_TetrahedronVertices = new Vector3[]
		{
			new Vector3(0f, -height, 0.5f),
			new Vector3(0f, -height, -0.5f),
			new Vector3(0.5f, height, 0f),
			new Vector3(-0.5f, height, 0f)
		};
		m_TetrahedronIndices = new int[]
		{
			0, 1, 2,
			1, 3, 2,
			2, 3, 0,
			3, 1, 0
		};
		m_MeshFilter = GetComponent<MeshFilter>();
	}

	Mesh CreatePolyhedron(Vector3[] vertices, int[] indices)
	{
		Mesh mesh = new Mesh();
		mesh.vertices = vertices;
		Color[] colors = new Color[vertices.Length];
		for (int i = 0; i < vertices.Length; i++)
		{
			colors[i] = new Color(vertices[i].x + 0.5f, vertices[i].y + 0.5f, vertices[i].z + 0.5f);
		}
		
		mesh.triangles = indices;
		mesh.RecalculateNormals();
		return mesh;
	}

	void TesselatePolyhedron(Mesh mesh)
	{
		List<Vector3> vertices = new List<Vector3>(mesh.vertices);
		List<int> indices = new List<int>(mesh.triangles);
		List<int> newIndices = new List<int>();
		float vertexLength = Mathf.Sqrt(0.375f);
		for (int t = 0; t < indices.Count; t += 3)
		{
			int idx0 = indices[t];
			int idx1 = indices[t + 1];
			int idx2 = indices[t + 2];
			Vector3 v0 = vertices[idx0];
			Vector3 v1 = vertices[idx1];
			Vector3 v2 = vertices[idx2];

			Vector3 center0 = (v0 + v1) / 2f;
			Vector3 center1 = (v1 + v2) / 2f;
			Vector3 center2 = (v0 + v2) / 2f;

			center0 = center0.normalized * vertexLength;
			center1 = center1.normalized * vertexLength;
			center2 = center2.normalized * vertexLength;

			int count = vertices.Count;
			vertices.Add(center0);
			vertices.Add(center1);
			vertices.Add(center2);

			newIndices.AddRange(new []{ idx0, count, count + 2 });
			newIndices.AddRange(new []{ idx2, count + 2, count + 1 });
			newIndices.AddRange(new []{ idx1, count + 1, count });
			newIndices.AddRange(new []{ count, count + 1, count + 2 });
		}
		mesh.SetVertices(vertices);
		mesh.SetTriangles(newIndices, 0);
		mesh.RecalculateNormals();
	}

	protected override void Tween(float value)
	{
		int iterations = Mathf.CeilToInt(value * 4);
		if (iterations > 4)
			iterations = 4;
		{
			Mesh m = CreatePolyhedron(m_TetrahedronVertices, m_TetrahedronIndices);
			for (int i = 0; i < iterations; i++)
			{
				TesselatePolyhedron(m);
			}
			m_MeshFilter.sharedMesh = m;
		}
	}
}
