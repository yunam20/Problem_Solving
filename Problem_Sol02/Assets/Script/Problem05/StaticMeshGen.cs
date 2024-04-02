using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StaticMeshGen))]
public class StaticMeshGenEditor : Editor
{
    //��ư����� ����
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        StaticMeshGen script = (StaticMeshGen)target;

        if (GUILayout.Button("Generate Mesh"))
        {
            script.GenerateMesh();
        }

    }
}

//�޽������ ����
public class StaticMeshGen : MonoBehaviour
{
    // Start is called before the first frame update
    public void GenerateMesh()
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[]
        {
            new Vector3 (0.0f, 0.0f, 0.0f), //0
            new Vector3 (-1.0f, -2.0f, 0.0f), //1
            new Vector3 (1.0f, -2.0f, 0.0f), //2
            new Vector3 (3.0f, -2.0f, 0.0f), //3
            new Vector3 (2.0f, -4.0f, 0.0f), //4
            new Vector3 (3.0f, -6.0f, 0.0f), //5
            new Vector3 (0f, -5.0f, 0.0f), //6
            new Vector3 (-3.0f, -6.0f, 0.0f), //7
            new Vector3 (-2.0f, -4.0f, 0.0f), //8
            new Vector3 (-3.0f, -2.0f, 0.0f), //9
            new Vector3 (-1.0f, -2.0f, 0.0f), //10
        };

        mesh.vertices = vertices;

        int[] triangleIndices = new int[]
        {
            0,2,1,
            2,3,4,
            4,5,6,
            6,7,8,
            8,9,10,
            6,8,10,
            10,2,6,
            4,6,2
        };

        mesh.triangles = triangleIndices;

        MeshFilter mf = this.AddComponent<MeshFilter>();
        MeshRenderer mr = this.AddComponent<MeshRenderer>();

        mf.mesh = mesh;
    }
}