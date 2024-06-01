using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AABBTest : MonoBehaviour
{
    public Transform box1;
    public Transform box2;

    void Update()
    {
        // �� �ڽ��� Bounds�� �����ɴϴ�.
        Bounds bounds1 = box1.GetComponent<BoxCollider>().bounds;
        Bounds bounds2 = box2.GetComponent<BoxCollider>().bounds;

        // AABB �浹 �˻�, �ش� �ڵ带 ����ϸ� Rigidbody ���̵� �浹 �˻簡 �����մϴ�
        /*
        if (bounds1.Intersects(bounds2))
        {
            Debug.Log("Boxes are colliding!");
        }
        else
        {
            Debug.Log("No collision.");
        }*/

        if (MyIntersects(bounds1, bounds2))
        {
            Debug.Log("Boxes are colliding!");
        }
        else
        {
            Debug.Log("No collision.");
        }
    }

    public bool MyIntersects(Bounds b1, Bounds b2)
    {
        // �� �࿡ ���� �� Bounds�� �����ϴ��� Ȯ��
        bool xOverlap = b1.max.x >= b2.min.x && b1.min.x <= b2.max.x;
        bool yOverlap = b1.max.y >= b2.min.y && b1.min.y <= b2.max.y;
        bool zOverlap = b1.max.z >= b2.min.z && b1.min.z <= b2.max.z;

        return xOverlap && yOverlap && zOverlap; // ��� �࿡�� ��ħ�� �־�� true
    }
}
