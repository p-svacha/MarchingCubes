using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshData
{
    public List<Vector3> Vertices; // Array of vertex positions
    public List<Vector2> UVs; // Relative position of each vertex on the mesh between 0 and 1
    public List<int> Triangles; // Array of vertex Ids (position in vertices array) that the mesh triangles are formed from, 3 vertices -> 1 triange

    public int VertexId;

    public MeshData()
    {
        Vertices = new List<Vector3>();
        UVs = new List<Vector2>();
        Triangles = new List<int>();
        VertexId = 0;
    }

    /// <summary>
    /// x, y, z are the world position of the vertex
    /// </summary>
    private int AddVertex(Vector3 vertex)
    {
        Vertices.Add(vertex);
        return VertexId++;
    }

    /// <summary>
    /// x and y are the relative position of the vertex with the same index on the map (0-1)
    /// </summary>
    private void AddUV(float x, float y)
    {
        UVs.Add(new Vector2(x, y));
    }

    /// <summary>
    /// a, b, c refer to vertex ids as they are saved in the array
    /// </summary>
    public void AddTriangle(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        int v1Id = AddVertex(p1);
        int v2Id = AddVertex(p2);
        int v3Id = AddVertex(p3);
        Triangles.Add(v1Id);
        Triangles.Add(v2Id);
        Triangles.Add(v3Id);
    }

    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = Vertices.ToArray();
        mesh.triangles = Triangles.ToArray();
        mesh.uv = UVs.ToArray();
        mesh.RecalculateNormals();
        return mesh;
    }
}