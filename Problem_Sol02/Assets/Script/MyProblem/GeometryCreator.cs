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

        // ���ؽ� �迭 (���� ��ġ��)
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(0, 0, 0),  // Point
            new Vector3(0, 1, 0),  // Line�� ������
            new Vector3(1, 2, 0),  // Line�� ����
            new Vector3(2, 0, 0),  // Triangle�� ù ��° ������
            new Vector3(2, 1, 0),  // Triangle�� �� ��° ������
            new Vector3(3, 0, 0)   // Triangle�� �� ��° ������
        };

        // Ʈ���̾ޱ� �迭 (�ﰢ���� ����� �ε���)
        int[] triangles = new int[]
        {
            3, 4, 5  // Triangle�� ����� �ε���
        };

        // ���� �迭
        Color[] colors = new Color[vertices.Length];
        colors[0] = Color.red;    // Point
        colors[1] = Color.green;  // Line�� ������
        colors[2] = Color.blue;   // Line�� ����
        colors[3] = Color.yellow; // Triangle�� ù ��° ������
        colors[4] = Color.magenta;// Triangle�� �� ��° ������
        colors[5] = Color.cyan;   // Triangle�� �� ��° ������

        // �޽� ����
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors;

        // �޽� ����
        MeshFilter mf = this.AddComponent<MeshFilter>();
        MeshRenderer mr = this.AddComponent<MeshRenderer>();

        mf.mesh = mesh;
    }
}
