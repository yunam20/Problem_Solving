using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuarterViewCamera : MonoBehaviour
{
    public Transform target; // 카메라가 바라볼 타겟 오브젝트
    public Vector3 offset = new Vector3(0, 10, -10); // 타겟 대비 카메라의 위치 오프셋
    public float rotationAngleX = 30.0f; // X축 기준 회전 각도
    public float rotationAngleY = 45.0f; // Y축 기준 회전 각도

    void Update()
    {
        // 카메라 위치 초기화
        transform.position = target.position + offset;

        // 카메라 회전 설정
        transform.rotation = Quaternion.Euler(rotationAngleX, rotationAngleY, 0);

        // 타겟을 바라보게 설정
        transform.LookAt(target);
    }
}