using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleCollisionDetector : MonoBehaviour
{
    public Transform circle1;
    public Transform circle2;
    public float radius1 = 1.0f;
    public float radius2 = 1.0f;

    void Update()
    {
        // �� ���� �߽� �� �Ÿ��� ���
        float distance = Vector3.Distance(circle1.position, circle2.position);

        // �������� ��
        float radiusSum = radius1 + radius2;

        // �浹 �˻�
        if (distance <= radiusSum)
        {
            Debug.Log("Circles are colliding!");
        }
        else
        {
            Debug.Log("No collision.");
        }
    }

    // Gizmos�� ����Ͽ� �� ���� �������� �׸���
    void OnDrawGizmos()
    {
        if (circle1 != null)
        {
            Gizmos.color = Color.red; // ù ��° ���� �� ����
            Gizmos.DrawWireSphere(circle1.position, radius1); // ù ��° �� �׸���
        }

        if (circle2 != null)
        {
            Gizmos.color = Color.blue; // �� ��° ���� �� ����
            Gizmos.DrawWireSphere(circle2.position, radius2); // �� ��° �� �׸���
        }
    }
}
