using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleMeshGenerator : MonoBehaviour
{
    public float thickness = 0.1f; // The thickness of the triangle

    void Start()
    {
        CreateTriangle();
    }

    void CreateTriangle()
    {
        // Create a new mesh
        Mesh mesh = new Mesh();

        // Define vertices for a triangle (two layers for thickness)
        Vector3[] vertices = new Vector3[6]; // 3 for front, 3 for back
        vertices[0] = new Vector3(0, 0, 0);       // Vertex 1 (front)
        vertices[1] = new Vector3(1, 0, 0);       // Vertex 2 (front)
        vertices[2] = new Vector3(0.5f, 1, 0);    // Vertex 3 (front)
        vertices[3] = new Vector3(0, 0, -thickness);  // Vertex 1 (back)
        vertices[4] = new Vector3(1, 0, -thickness);  // Vertex 2 (back)
        vertices[5] = new Vector3(0.5f, 1, -thickness); // Vertex 3 (back)

        // Define triangles (order of vertices)
        int[] triangles = new int[18];
        // Front face
        triangles[0] = 0; triangles[1] = 1; triangles[2] = 2;
        // Back face
        triangles[3] = 3; triangles[4] = 5; triangles[5] = 4;
        // Left side
        triangles[6] = 0; triangles[7] = 3; triangles[8] = 2;
        triangles[9] = 2; triangles[10] = 3; triangles[11] = 5;
        // Right side
        triangles[12] = 1; triangles[13] = 4; triangles[14] = 5;
        triangles[15] = 1; triangles[16] = 5; triangles[17] = 2;

        // Assign vertices and triangles to the mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        // Recalculate normals for lighting
        mesh.RecalculateNormals();

        // Add a MeshFilter and MeshRenderer to the GameObject
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        // Create a material with the Standard Shader and assign a yellow color
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        Material newMaterial = new Material(Shader.Find("Standard")); // Using Unity's Standard Shader
        newMaterial.color = Color.yellow; // Set the material color to yellow

        meshRenderer.material = newMaterial; // Assign the new material to the renderer
    }
}
