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
        // 두 원의 중심 간 거리를 계산
        float distance = Vector3.Distance(circle1.position, circle2.position);

        // 반지름의 합
        float radiusSum = radius1 + radius2;

        // 충돌 검사
        if (distance <= radiusSum)
        {
            Debug.Log("Circles are colliding!");
        }
        else
        {
            Debug.Log("No collision.");
        }
    }

    // Gizmos를 사용하여 각 원의 반지름을 그리기
    void OnDrawGizmos()
    {
        if (circle1 != null)
        {
            Gizmos.color = Color.red; // 첫 번째 원의 색 설정
            Gizmos.DrawWireSphere(circle1.position, radius1); // 첫 번째 원 그리기
        }

        if (circle2 != null)
        {
            Gizmos.color = Color.blue; // 두 번째 원의 색 설정
            Gizmos.DrawWireSphere(circle2.position, radius2); // 두 번째 원 그리기
        }
    }
}
