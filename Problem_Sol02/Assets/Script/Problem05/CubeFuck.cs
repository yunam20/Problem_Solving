using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CustomEditor(typeof(CubeFuck))]
public class CubeGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CubeFuck script = (CubeFuck)target;

        if (GUILayout.Button("Generate Cube"))
        {
            script.GenerateCube();
        }
    }
}

public class CubeFuck : MonoBehaviour
{
    public void GenerateCube()
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[]
        {
            new Vector3(-0.5f, -0.5f, -0.5f), // 0
            new Vector3(0.5f, -0.5f, -0.5f),  // 1
            new Vector3(0.5f, -0.5f, 0.5f),   // 2
            new Vector3(-0.5f, -0.5f, 0.5f),  // 3
            new Vector3(-0.5f, 0.5f, -0.5f),  // 4
            new Vector3(0.5f, 0.5f, -0.5f),   // 5
            new Vector3(0.5f, 0.5f, 0.5f),    // 6
            new Vector3(-0.5f, 0.5f, 0.5f)    // 7
        };

        int[] triangleIndices = new int[]
        {
            // Bottom face
            0, 2, 1,
            0, 3, 2,

            // Top face
            4, 5, 6,
            4, 6, 7,

            // Side faces
            0, 1, 5,
            0, 5, 4,
            1, 2, 6,
            1, 6, 5,
            2, 3, 7,
            2, 7, 6,
            3, 0, 4,
            3, 4, 7
        };

        Vector3[] normals = new Vector3[vertices.Length];

        // Compute normals for each triangle
        for (int i = 0; i < triangleIndices.Length; i += 3)
        {
            int index0 = triangleIndices[i];
            int index1 = triangleIndices[i + 1];
            int index2 = triangleIndices[i + 2];

            Vector3 side1 = vertices[index1] - vertices[index0];
            Vector3 side2 = vertices[index2] - vertices[index0];
            Vector3 normal = Vector3.Cross(side1, side2).normalized;

            normals[index0] += normal;
            normals[index1] += normal;
            normals[index2] += normal;
        }

        // Normalize normals
        for (int i = 0; i < normals.Length; i++)
        {
            normals[i] = normals[i].normalized;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangleIndices;
        mesh.normals = normals;

        MeshFilter mf = gameObject.AddComponent<MeshFilter>();
        MeshRenderer mr = gameObject.AddComponent<MeshRenderer>();

        mf.mesh = mesh;
    }
}