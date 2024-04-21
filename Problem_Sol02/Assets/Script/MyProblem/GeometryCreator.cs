using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[CustomEditor(typeof(GeometryCreator))]

public class GeometryCreatorEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GeometryCreator script = (GeometryCreator)target;

        if (GUILayout.Button("Generate Mesh"))
        {
            script.CreateGeometry();
        }

    }
}

public class GeometryCreator : MonoBehaviour
{
    public void CreateGeometry()
    {
        Mesh mesh = new Mesh();

        // 버텍스 배열 (점의 위치들)
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(0, 0, 0),  // Point
            new Vector3(0, 1, 0),  // Line의 시작점
            new Vector3(1, 2, 0),  // Line의 끝점
            new Vector3(2, 0, 0),  // Triangle의 첫 번째 꼭지점
            new Vector3(2, 1, 0),  // Triangle의 두 번째 꼭지점
            new Vector3(3, 0, 0)   // Triangle의 세 번째 꼭지점
        };

        // 트라이앵글 배열 (삼각형을 만드는 인덱스)
        int[] triangles = new int[]
        {
            3, 4, 5  // Triangle을 만드는 인덱스
        };

        // 색상 배열
        Color[] colors = new Color[vertices.Length];
        colors[0] = Color.red;    // Point
        colors[1] = Color.green;  // Line의 시작점
        colors[2] = Color.blue;   // Line의 끝점
        colors[3] = Color.yellow; // Triangle의 첫 번째 꼭지점
        colors[4] = Color.magenta;// Triangle의 두 번째 꼭지점
        colors[5] = Color.cyan;   // Triangle의 세 번째 꼭지점

        // 메시 설정
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors;

        // 메시 적용
        MeshFilter mf = this.AddComponent<MeshFilter>();
        MeshRenderer mr = this.AddComponent<MeshRenderer>();

        mf.mesh = mesh;
    }
}
