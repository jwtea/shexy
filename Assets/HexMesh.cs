using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class HexMesh : MonoBehaviour { 
    Mesh hexMesh;
    List<Vector3> vertices;
    List<int> triangles;
    List<Color> colors;
    MeshCollider meshCollider;

    void Awake() {
        GetComponent<MeshFilter>().mesh = hexMesh = new Mesh();
        meshCollider = gameObject.AddComponent<MeshCollider>();
        hexMesh.name = "Hex Mesh";
        vertices = new List<Vector3>();
        colors = new List<Color>();
        triangles = new List<int>();
    }

        // Triangulate all cells
    public void Triangulate (HexCell[] cells) {
        hexMesh.Clear();
        vertices.Clear();
        triangles.Clear();
        colors.Clear();
        
        for (int i = 0; i < cells.Length; i++) {
            Triangulate(cells[i]);
        }

        hexMesh.vertices = vertices.ToArray();
        hexMesh.colors = colors.ToArray();
        hexMesh.triangles = triangles.ToArray();
        hexMesh.RecalculateNormals();
        meshCollider.sharedMesh = hexMesh;
    }
    // Triangulate a cell
    void Triangulate (HexCell cell) {
        //Foreach direction triangulate a cell
        for (HexDirection d = HexDirection.NE; d <= HexDirection.NW; d++) {
            Triangulate(d, cell);
        }
    }
    // Add a triangle using the hex metrics to get the vertex positions
    void Triangulate (HexDirection direction, HexCell cell) {
        Vector3 center = cell.transform.localPosition;
        AddTriangle(
            center,
            center + HexMetrics.GetFirstCorner(direction),
            center + HexMetrics.GetSecondCorner(direction)
        );

        //Blend colors from neighbors
        //Use current cell if no neighbor in direction
        HexCell prevNeighbor = cell.GetNeighbor(direction.Previous()) ?? cell;
        HexCell neighbor = cell.GetNeighbor(direction) ?? cell;
        HexCell nextNeighbor = cell.GetNeighbor(direction.Next()) ?? cell;

        AddTriangleColor(
            cell.color, 
            (cell.color + prevNeighbor.color + neighbor.color) / 3f, 
            (cell.color + nextNeighbor.color + neighbor.color) / 3f);
    }
    
    // Add a triangle 
    void AddTriangle (Vector3 v1, Vector3 v2, Vector3 v3) {
        int vertexIndex = vertices.Count;
        vertices.Add(v1);
        vertices.Add(v2);
        vertices.Add(v3);
        
        triangles.Add(vertexIndex);
        triangles.Add(vertexIndex + 1);
        triangles.Add(vertexIndex + 2);
    }

    // Add color to each vertex of the triangle
    void AddTriangleColor (Color c1, Color c2, Color c3) {
        colors.Add(c1);
        colors.Add(c2);
        colors.Add(c3);
    }
}