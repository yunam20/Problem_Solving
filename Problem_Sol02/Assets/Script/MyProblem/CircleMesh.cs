using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMesh : MonoBehaviour
{
    public Transform circle1;
    public Transform circle2;
    public float radius1 = 1.0f;
    public float radius2 = 1.0f;
    private Mesh mesh1;
    private Mesh mesh2;

    void Start()
    {
        // �޽� �ʱ�ȭ �� ����
        mesh1 = CreateCircleMesh(radius1);
        mesh2 = CreateCircleMesh(radius2);
    }

    void Update()
    {
        // �޽� ������, ���� ���η� ȸ��
        Graphics.DrawMesh(mesh1, circle1.position, Quaternion.identity, new Material(Shader.Find("Standard")), 0);
        Graphics.DrawMesh(mesh2, circle2.position, Quaternion.identity, new Material(Shader.Find("Standard")), 0);
    }

    Mesh CreateCircleMesh(float radius)
    {
        Mesh mesh = new Mesh();
        const int segments = 36; // ���� ���׸�Ʈ ��
        Vector3[] vertices = new Vector3[segments + 1];
        int[] triangles = new int[segments * 3];

        vertices[0] = Vector3.zero; // �߽���
        for (int i = 0; i < segments; i++)
        {
            float angle = i * Mathf.PI * 2f / segments;
            vertices[i + 1] = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
            if (i < segments - 1)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        triangles[segments * 3 - 3] = 0;
        triangles[segments * 3 - 2] = segments;
        triangles[segments * 3 - 1] = 1;

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        
        // �޽��� ���� ���� ���� ���� �迭 ����
        Color[] colors = new Color[mesh.vertexCount];

        // ��� ������ ���� ������ ����
        for (int i = 0; i < mesh.vertexCount; i++)
        {
            colors[i] = Color.red;
        }

        // �޽��� ���� �Ӽ��� �迭 �Ҵ�
        mesh.colors = colors;
        
        mesh.RecalculateNormals();

        return mesh;
    }
}
