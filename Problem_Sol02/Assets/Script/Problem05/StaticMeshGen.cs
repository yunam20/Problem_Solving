using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StaticMeshGen))]
public class StaticMeshGenEditor : Editor
{
    //버튼만들기 예제
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

//메쉬만들기 예제
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

            new Vector3 (0.0f, 0.0f, -10.0f), //11
            new Vector3 (-1.0f, -2.0f, -10.0f), //12
            new Vector3 (1.0f, -2.0f, -10.0f), //13
            new Vector3 (3.0f, -2.0f, -10.0f), //14
            new Vector3 (2.0f, -4.0f, -10.0f), //15
            new Vector3 (3.0f, -6.0f, -10.0f), //16
            new Vector3 (0f, -5.0f, -10.0f), //17
            new Vector3 (-3.0f, -6.0f, -10.0f), //18
            new Vector3 (-2.0f, -4.0f, -10.0f), //19
            new Vector3 (-3.0f, -2.0f, -10.0f), //20
            new Vector3 (-1.0f, -2.0f, -10.0f), //21
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
            4,6,2,

            11,13,12,
            13,14,15,
            15,16,17,
            17,18,19,
            19,20,21,
            17,19,21,
            21,13,17,
            15,17,13
        };

        mesh.triangles = triangleIndices;

        MeshFilter mf = this.AddComponent<MeshFilter>();
        MeshRenderer mr = this.AddComponent<MeshRenderer>();

        mf.mesh = mesh;
    }
}
